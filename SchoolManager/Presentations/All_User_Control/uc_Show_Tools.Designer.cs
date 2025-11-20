namespace SchoolManager.Presentations.All_User_Control
{
    partial class uc_Show_Tools
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uc_Show_Tools));
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2Button4 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panel_Container_Tools = new Guna.UI2.WinForms.Guna2Panel();
            this.uc_Drawing_Board1 = new SchoolManager.Presentations.uc_Drawing_Board();
            this.uc_Time_Management_Tool1 = new SchoolManager.Presentations.All_User_Control.uc_Time_Management_Tool();
            this.guna2Panel1.SuspendLayout();
            this.panel_Container_Tools.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.Controls.Add(this.guna2Button4);
            this.guna2Panel1.Controls.Add(this.guna2Button1);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(2077, 112);
            this.guna2Panel1.TabIndex = 1;
            // 
            // guna2Button4
            // 
            this.guna2Button4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.guna2Button4.BorderRadius = 20;
            this.guna2Button4.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button4.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button4.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button4.FillColor = System.Drawing.Color.MediumPurple;
            this.guna2Button4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold);
            this.guna2Button4.ForeColor = System.Drawing.Color.White;
            this.guna2Button4.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button4.Image")));
            this.guna2Button4.ImageOffset = new System.Drawing.Point(0, 3);
            this.guna2Button4.ImageSize = new System.Drawing.Size(60, 60);
            this.guna2Button4.Location = new System.Drawing.Point(1279, 13);
            this.guna2Button4.Name = "guna2Button4";
            this.guna2Button4.Size = new System.Drawing.Size(324, 84);
            this.guna2Button4.TabIndex = 32;
            this.guna2Button4.Text = "Bảng vẽ điện tử";
            this.guna2Button4.Click += new System.EventHandler(this.guna2Button4_Click);
            // 
            // guna2Button1
            // 
            this.guna2Button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.guna2Button1.BorderRadius = 20;
            this.guna2Button1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2Button1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2Button1.FillColor = System.Drawing.Color.CornflowerBlue;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.125F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.Image = ((System.Drawing.Image)(resources.GetObject("guna2Button1.Image")));
            this.guna2Button1.ImageOffset = new System.Drawing.Point(0, 3);
            this.guna2Button1.ImageSize = new System.Drawing.Size(50, 50);
            this.guna2Button1.Location = new System.Drawing.Point(453, 13);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.Size = new System.Drawing.Size(350, 84);
            this.guna2Button1.TabIndex = 30;
            this.guna2Button1.Text = "Đồng hồ đếm ngược";
            this.guna2Button1.TextOffset = new System.Drawing.Point(-5, 0);
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.TargetControl = this;
            // 
            // panel_Container_Tools
            // 
            this.panel_Container_Tools.Controls.Add(this.uc_Drawing_Board1);
            this.panel_Container_Tools.Controls.Add(this.uc_Time_Management_Tool1);
            this.panel_Container_Tools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Container_Tools.Location = new System.Drawing.Point(0, 112);
            this.panel_Container_Tools.Name = "panel_Container_Tools";
            this.panel_Container_Tools.Size = new System.Drawing.Size(2077, 976);
            this.panel_Container_Tools.TabIndex = 2;
            // 
            // uc_Drawing_Board1
            // 
            this.uc_Drawing_Board1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.uc_Drawing_Board1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_Drawing_Board1.Location = new System.Drawing.Point(0, 0);
            this.uc_Drawing_Board1.Name = "uc_Drawing_Board1";
            this.uc_Drawing_Board1.Size = new System.Drawing.Size(2077, 976);
            this.uc_Drawing_Board1.TabIndex = 1;
            this.uc_Drawing_Board1.Visible = false;
            // 
            // uc_Time_Management_Tool1
            // 
            this.uc_Time_Management_Tool1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(250)))));
            this.uc_Time_Management_Tool1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_Time_Management_Tool1.Location = new System.Drawing.Point(0, 0);
            this.uc_Time_Management_Tool1.Name = "uc_Time_Management_Tool1";
            this.uc_Time_Management_Tool1.Size = new System.Drawing.Size(2077, 976);
            this.uc_Time_Management_Tool1.TabIndex = 0;
            this.uc_Time_Management_Tool1.Visible = false;
            // 
            // uc_Show_Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_Container_Tools);
            this.Controls.Add(this.guna2Panel1);
            this.Name = "uc_Show_Tools";
            this.Size = new System.Drawing.Size(2077, 1088);
            this.guna2Panel1.ResumeLayout(false);
            this.panel_Container_Tools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2Button guna2Button4;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private Guna.UI2.WinForms.Guna2Panel panel_Container_Tools;
        private uc_Drawing_Board uc_Drawing_Board1;
        private uc_Time_Management_Tool uc_Time_Management_Tool1;
    }
}
