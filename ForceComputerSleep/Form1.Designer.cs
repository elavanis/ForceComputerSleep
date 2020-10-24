namespace ForceComputerSleep
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label_CountDownTimer = new System.Windows.Forms.Label();
            this.button_ForceSleep = new System.Windows.Forms.Button();
            this.numericUpDown_Delay = new System.Windows.Forms.NumericUpDown();
            this.label_Delay = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_NotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Delay)).BeginInit();
            this.contextMenuStrip_NotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_CountDownTimer
            // 
            this.label_CountDownTimer.AutoSize = true;
            this.label_CountDownTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_CountDownTimer.Location = new System.Drawing.Point(12, 115);
            this.label_CountDownTimer.Name = "label_CountDownTimer";
            this.label_CountDownTimer.Size = new System.Drawing.Size(417, 108);
            this.label_CountDownTimer.TabIndex = 0;
            this.label_CountDownTimer.Text = "00:30:00";
            // 
            // button_ForceSleep
            // 
            this.button_ForceSleep.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ForceSleep.Image = global::ForceComputerSleep.Properties.Resources.Sleep;
            this.button_ForceSleep.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button_ForceSleep.Location = new System.Drawing.Point(12, 12);
            this.button_ForceSleep.Name = "button_ForceSleep";
            this.button_ForceSleep.Size = new System.Drawing.Size(136, 100);
            this.button_ForceSleep.TabIndex = 1;
            this.button_ForceSleep.Text = "Sleep\r\nNow";
            this.button_ForceSleep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button_ForceSleep.UseVisualStyleBackColor = true;
            this.button_ForceSleep.Click += new System.EventHandler(this.ForceSleep_Click);
            // 
            // numericUpDown_Delay
            // 
            this.numericUpDown_Delay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_Delay.Location = new System.Drawing.Point(276, 50);
            this.numericUpDown_Delay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown_Delay.Name = "numericUpDown_Delay";
            this.numericUpDown_Delay.Size = new System.Drawing.Size(120, 26);
            this.numericUpDown_Delay.TabIndex = 2;
            // 
            // label_Delay
            // 
            this.label_Delay.AutoSize = true;
            this.label_Delay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Delay.Location = new System.Drawing.Point(219, 52);
            this.label_Delay.Name = "label_Delay";
            this.label_Delay.Size = new System.Drawing.Size(49, 20);
            this.label_Delay.TabIndex = 3;
            this.label_Delay.Text = "Delay";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip_NotifyIcon;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "ForceComputerSleep";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // contextMenuStrip_NotifyIcon
            // 
            this.contextMenuStrip_NotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.contextMenuStrip_NotifyIcon.Name = "contextMenuStrip_NotifyIcon";
            this.contextMenuStrip_NotifyIcon.Size = new System.Drawing.Size(94, 26);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Exit);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 226);
            this.Controls.Add(this.label_Delay);
            this.Controls.Add(this.numericUpDown_Delay);
            this.Controls.Add(this.button_ForceSleep);
            this.Controls.Add(this.label_CountDownTimer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Force Computer To Sleep";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Delay)).EndInit();
            this.contextMenuStrip_NotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_CountDownTimer;
        private System.Windows.Forms.Button button_ForceSleep;
        private System.Windows.Forms.NumericUpDown numericUpDown_Delay;
        private System.Windows.Forms.Label label_Delay;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_NotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

