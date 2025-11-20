using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolManager.Presentations.All_User_Control
{
    public partial class uc_Time_Management_Tool : UserControl
    {
        // Countdown timer fields
        private Timer countdownTimer;
        private TimeSpan remaining = TimeSpan.Zero;
        private bool isRunning = false;

        public uc_Time_Management_Tool()
        {
            InitializeComponent();

            // Initialize timer
            countdownTimer = new Timer();
            countdownTimer.Interval = 1000; //1 second
            countdownTimer.Tick += CountdownTimer_Tick;

            // Wire up control events
            this.Load += Uc_Time_Management_Tool_Load;

            // Buttons (designer names)
            this.StartButton.Click += StartButton_Click; // Bắt đầu
            this.PauseButton.Click += PauseButton_Click; // Tạm dừng
            this.ResetButton.Click += ResetButton_Click; // Đặt lại

            // Preset quick buttons
            this.guna2GradientButton13.Click += (s, e) => PresetButton_Click(s, e, 5);
            this.guna2GradientButton1.Click += (s, e) => PresetButton_Click(s, e, 10);
            this.guna2GradientButton2.Click += (s, e) => PresetButton_Click(s, e, 15);
            this.guna2GradientButton3.Click += (s, e) => PresetButton_Click(s, e, 30);
            this.guna2GradientButton4.Click += (s, e) => PresetButton_Click(s, e, 45);

            // Initial display
            UpdateDisplay(TimeSpan.Zero);
        }

        private void Uc_Time_Management_Tool_Load(object sender, EventArgs e)
        {
            // Ensure initial label shows00 :00
            UpdateDisplay(remaining);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (isRunning) return;

            // If nothing set, try parse inputs
            if (remaining == TimeSpan.Zero)
            {
                int minutes = 0, seconds = 0;
                int.TryParse(guna2TextBox4.Text, out minutes);
                int.TryParse(guna2TextBox1.Text, out seconds);

                remaining = TimeSpan.FromMinutes(minutes) + TimeSpan.FromSeconds(seconds);
            }

            if (remaining.TotalSeconds > 0)
            {
                countdownTimer.Start();
                isRunning = true;
            }
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                countdownTimer.Stop();
                isRunning = false;
            }
            else
            {
                // Resume if there's remaining time
                if (remaining.TotalSeconds > 0)
                {
                    countdownTimer.Start();
                    isRunning = true;
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            countdownTimer.Stop();
            isRunning = false;
            remaining = TimeSpan.Zero;
            UpdateDisplay(remaining);
        }

        private void PresetButton_Click(object sender, EventArgs e, int minutes)
        {
            // Set remaining from preset and start
            remaining = TimeSpan.FromMinutes(minutes);
            UpdateDisplay(remaining);
            countdownTimer.Start();
            isRunning = true;
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (remaining.TotalSeconds <= 1)
            {
                countdownTimer.Stop();
                remaining = TimeSpan.Zero;
                isRunning = false;
                UpdateDisplay(remaining);
                System.Media.SystemSounds.Exclamation.Play();
                return;
            }

            remaining = remaining.Subtract(TimeSpan.FromSeconds(1));
            UpdateDisplay(remaining);
        }

        private void UpdateDisplay(TimeSpan time)
        {
            // Show minutes : seconds (total minutes may exceed60)
            int totalSeconds = (int)time.TotalSeconds;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            // Format as "MM : SS" (minutes can be more than2 digits if long)
            guna2HtmlLabel3.Text = string.Format("{0:D2} : {1:D2}", minutes, seconds);
        }
    }
}