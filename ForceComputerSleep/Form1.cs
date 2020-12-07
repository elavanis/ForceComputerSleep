using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForceComputerSleep
{
    public partial class Form1 : Form
    {
        private TimeSpan shutDownTime;
        private static DateTime lastInput = DateTime.Now;
        private DateTime lastSleepSent = DateTime.Now;
        private DateTime lastJoystickCheck = DateTime.Now;
        private TimeSpan timeLeft = new TimeSpan(0, 30, 0);
        private static Timer timerForUIUpdate = new Timer();
        private static Timer timerForRepeatSleep = new Timer();
        private static bool AllowApplicationExit = false;

        private static LowLevelProc _procKeyboard = new LowLevelProc(HookCallback);
        private static LowLevelProc _procMouse = new LowLevelProc(HookCallback);
        private static IntPtr _hookIDKeyboard = IntPtr.Zero;
        private static IntPtr _hookIDMouse = IntPtr.Zero;
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;
        private const int WM_KEYDOWN = 256;
        private const int WM_MOUSEMOVE = 512;

        //Joysticks leak memory on creation so we will cache each one.
        //This way we wont won't create more than we need during each build
        //and we can reuse ones if they are unplugged and plugged back in.
        Dictionary<Guid, Joystick> joystickDictionary = new Dictionary<Guid, Joystick>();

        List<Joystick> joysticks;

        public Form1()
        {
            InitializeComponent();

            shutDownTime = TimeSpan.Parse(ConfigurationManager.AppSettings["ShutDownTime"]);
            _hookIDKeyboard = SetHook(_procKeyboard, WH_KEYBOARD_LL);
            _hookIDMouse = SetHook(_procMouse, WH_MOUSE_LL);

            joysticks = BuildJoystickList();

            timerForUIUpdate.Tick += Tick;
            timerForUIUpdate.Start();
            timerForRepeatSleep.Tick += RepeatSleep;
        }

        private List<Joystick> BuildJoystickList()
        {
            lastJoystickCheck = DateTime.Now;
            List<Guid> guids = new List<Guid>();

            // Initialize DirectInput
            var directInput = new DirectInput();

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                guids.Add(deviceInstance.InstanceGuid);
            }

            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
            {
                guids.Add(deviceInstance.InstanceGuid);
            }

            List<Joystick> joysticks = new List<Joystick>();

            foreach (var guid in guids)
            {
                Joystick joystick = null;

                if (joystickDictionary.TryGetValue(guid, out joystick))
                {
                    joysticks.Add(joystick);
                }
                else
                {
                    joystick = new Joystick(directInput, guid);
                    joystick.Properties.BufferSize = 128;
                    joystick.Acquire();
                    joysticks.Add(joystick);
                    joystickDictionary.Add(guid, joystick);
                }
            }

            return joysticks;
        }

        private void RepeatSleep(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute % 5 == 0
                && DateTime.Now.Second == 0
                && DateTime.Now.Subtract(lastSleepSent).TotalMinutes >= 5)
            {
                PutComputerToSleep();
            }
        }

        private void Tick(object sender, EventArgs e)
        {
            try
            {
                if (DateTime.Now.Subtract(lastJoystickCheck) > shutDownTime)
                {
                    //its been 30 minutes since we last updated joysticks, check to see if a new one has been plugged in
                    joysticks = BuildJoystickList();
                }

                CheckJoysticks();
            }
            catch
            {
                joysticks = BuildJoystickList();
            }

            timeLeft = shutDownTime.Subtract(DateTime.Now.Subtract(lastInput));
            timeLeft = timeLeft.Add(TimeSpan.FromMinutes((int)numericUpDown_Delay.Value));
            if (timeLeft.TotalSeconds <= 0.0)
            {
                PutComputerToSleep();
            }
            label_CountDownTimer.Text = timeLeft.ToString("hh") + ":" + timeLeft.ToString("mm") + ":" + timeLeft.ToString("ss");
        }

        private void CheckJoysticks()
        {
            foreach (var joystick in joysticks)
            {
                joystick.Poll();
                var datas = joystick.GetBufferedData();
                foreach (var state in datas)
                {
                    lastInput = DateTime.Now;
                    break;
                }
            }
        }

        private void PutComputerToSleep()
        {
            lastSleepSent = DateTime.Now;
            timerForUIUpdate.Stop();
            timerForRepeatSleep.Start();
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }

        private void ForceSleep_Click(object sender, EventArgs e)
        {
            PutComputerToSleep();
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN
                                || wParam == (IntPtr)WM_MOUSEMOVE))
            {
                //Console.WriteLine((object)(Keys)Marshal.ReadInt32(lParam));
                lastInput = DateTime.Now;

                if (timerForRepeatSleep.Enabled)
                {
                    timerForRepeatSleep.Stop();
                }

                if (!timerForUIUpdate.Enabled)
                {
                    timerForUIUpdate.Start();
                }
            }

            return CallNextHookEx(_hookIDKeyboard, nCode, wParam, lParam);
        }



        private IntPtr SetHook(LowLevelProc proc, int constToWatch)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule mainModule = currentProcess.MainModule)
                    return SetWindowsHookEx(constToWatch, proc, GetModuleHandle(mainModule.ModuleName), 0U);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing
                && !AllowApplicationExit)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void Exit(object sender, EventArgs e)
        {
            AllowApplicationExit = true;

            Application.Exit();
        }
    }
}