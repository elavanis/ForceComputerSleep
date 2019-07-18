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
        private TimeSpan timeLeft = new TimeSpan(0, 30, 0);
        private static Timer timerForUIUpdate = new Timer();

        private static LowLevelKeyboardProc _proc = new LowLevelKeyboardProc(HookCallback);
        private static IntPtr _hookID = IntPtr.Zero;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 256;

        public Form1()
        {
            InitializeComponent();

            shutDownTime = TimeSpan.Parse(ConfigurationManager.AppSettings["ShutDownTime"]);
            _hookID = SetHook(_proc);
            timerForUIUpdate.Tick += Tick;
            timerForUIUpdate.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            timeLeft = shutDownTime.Subtract(DateTime.Now.Subtract(lastInput));
            timeLeft = timeLeft.Add(TimeSpan.FromMinutes((int)numericUpDown_Delay.Value));
            if (timeLeft.TotalSeconds <= 0.0)
            {
                PutComputerToSleep();
            }
            label_CountDownTimer.Text = timeLeft.ToString("hh") + ":" + timeLeft.ToString("mm") + ":" + timeLeft.ToString("ss");
        }

        private void PutComputerToSleep()
        {
            timerForUIUpdate.Stop();
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }

        private void ForceSleep_Click(object sender, EventArgs e)
        {
            PutComputerToSleep();
        }

        private void UpdateCountDownTimer(object sender, EventArgs e)
        {

        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)256)
            {
                Console.WriteLine((object)(Keys)Marshal.ReadInt32(lParam));
                lastInput = DateTime.Now;
                if (!timerForUIUpdate.Enabled)
                {
                    timerForUIUpdate.Start();
                }

            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }



        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process currentProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule mainModule = currentProcess.MainModule)
                    return SetWindowsHookEx(13, proc, GetModuleHandle(mainModule.ModuleName), 0U);
            }
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);


    }
}