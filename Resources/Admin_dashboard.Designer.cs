namespace Hotel.Resources
{
    partial class Admin_dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin_dashboard));
            label1 = new Label();
            splitter1 = new Splitter();
            view_users = new Panel();
            button5 = new Button();
            view_booking = new Panel();
            button8 = new Button();
            change_pw = new Panel();
            button14 = new Button();
            button15 = new Button();
            User_manegement = new Button();
            button18 = new Button();
            button19 = new Button();
            button21 = new Button();
            button16 = new Button();
            splitter2 = new Splitter();
            label2 = new Label();
            pictureBox3 = new PictureBox();
            textBox1 = new TextBox();
            panel1 = new Panel();
            view_users.SuspendLayout();
            view_booking.SuspendLayout();
            change_pw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.MidnightBlue;
            label1.Font = new Font("Tahoma", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(369, 28);
            label1.Name = "label1";
            label1.Size = new Size(265, 34);
            label1.TabIndex = 0;
            label1.Text = "Admin Dashboard";
            label1.Click += label1_Click;
            // 
            // splitter1
            // 
            splitter1.BackColor = Color.MidnightBlue;
            splitter1.Location = new Point(0, 96);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(320, 588);
            splitter1.TabIndex = 1;
            splitter1.TabStop = false;
            // 
            // view_users
            // 
            view_users.BackColor = SystemColors.ActiveBorder;
            view_users.BackgroundImage = Properties.Resources.team;
            view_users.BackgroundImageLayout = ImageLayout.Zoom;
            view_users.BorderStyle = BorderStyle.Fixed3D;
            view_users.Controls.Add(button5);
            view_users.Location = new Point(356, 230);
            view_users.Name = "view_users";
            view_users.Size = new Size(185, 91);
            view_users.TabIndex = 13;
            // 
            // button5
            // 
            button5.BackColor = Color.DarkGreen;
            button5.ForeColor = Color.White;
            button5.Location = new Point(39, 43);
            button5.Name = "button5";
            button5.Size = new Size(124, 41);
            button5.TabIndex = 0;
            button5.Text = "View users";
            button5.UseVisualStyleBackColor = false;
            button5.Click += button5_Click;
            // 
            // view_booking
            // 
            view_booking.BackColor = SystemColors.ActiveBorder;
            view_booking.BackgroundImage = (Image)resources.GetObject("view_booking.BackgroundImage");
            view_booking.BackgroundImageLayout = ImageLayout.Zoom;
            view_booking.BorderStyle = BorderStyle.Fixed3D;
            view_booking.Controls.Add(button8);
            view_booking.Location = new Point(799, 434);
            view_booking.Name = "view_booking";
            view_booking.Size = new Size(195, 91);
            view_booking.TabIndex = 16;
            // 
            // button8
            // 
            button8.BackColor = Color.DarkGreen;
            button8.ForeColor = Color.White;
            button8.Location = new Point(34, 20);
            button8.Name = "button8";
            button8.Size = new Size(133, 40);
            button8.TabIndex = 0;
            button8.Text = "View booking";
            button8.UseVisualStyleBackColor = false;
            button8.Click += button8_Click;
            // 
            // change_pw
            // 
            change_pw.BackColor = SystemColors.ActiveBorder;
            change_pw.BackgroundImage = Properties.Resources.reset_password;
            change_pw.BackgroundImageLayout = ImageLayout.Zoom;
            change_pw.Controls.Add(button14);
            change_pw.Location = new Point(354, 570);
            change_pw.Name = "change_pw";
            change_pw.Size = new Size(185, 89);
            change_pw.TabIndex = 25;
            // 
            // button14
            // 
            button14.BackColor = Color.DarkGreen;
            button14.ForeColor = Color.White;
            button14.Location = new Point(36, 35);
            button14.Name = "button14";
            button14.Size = new Size(146, 41);
            button14.TabIndex = 0;
            button14.Text = "Change password";
            button14.UseVisualStyleBackColor = false;
            button14.Click += button14_Click;
            // 
            // button15
            // 
            button15.BackColor = Color.Red;
            button15.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button15.ForeColor = Color.White;
            button15.Location = new Point(1120, 28);
            button15.Name = "button15";
            button15.Size = new Size(94, 40);
            button15.TabIndex = 0;
            button15.Text = "Logout";
            button15.UseVisualStyleBackColor = false;
            button15.Click += button15_Click;
            // 
            // User_manegement
            // 
            User_manegement.BackColor = Color.MidnightBlue;
            User_manegement.ForeColor = Color.White;
            User_manegement.Location = new Point(83, 214);
            User_manegement.Name = "User_manegement";
            User_manegement.Size = new Size(157, 59);
            User_manegement.TabIndex = 28;
            User_manegement.Text = "User Management";
            User_manegement.UseVisualStyleBackColor = false;
            User_manegement.Click += button17_Click;
            // 
            // button18
            // 
            button18.BackColor = Color.MidnightBlue;
            button18.ForeColor = Color.White;
            button18.Location = new Point(83, 296);
            button18.Name = "button18";
            button18.Size = new Size(157, 66);
            button18.TabIndex = 29;
            button18.Text = "Booking Management";
            button18.UseVisualStyleBackColor = false;
            button18.Click += button18_Click;
            // 
            // button19
            // 
            button19.BackColor = Color.MidnightBlue;
            button19.ForeColor = Color.White;
            button19.Location = new Point(83, 390);
            button19.Name = "button19";
            button19.Size = new Size(157, 63);
            button19.TabIndex = 30;
            button19.Text = "Requests / Feedbacks";
            button19.UseVisualStyleBackColor = false;
            button19.Click += button19_Click;
            // 
            // button21
            // 
            button21.BackColor = Color.MidnightBlue;
            button21.ForeColor = Color.White;
            button21.Location = new Point(83, 483);
            button21.Name = "button21";
            button21.Size = new Size(157, 58);
            button21.TabIndex = 32;
            button21.Text = "Settings";
            button21.UseVisualStyleBackColor = false;
            button21.Click += button21_Click;
            // 
            // button16
            // 
            button16.BackColor = Color.MidnightBlue;
            button16.ForeColor = Color.White;
            button16.Location = new Point(83, 123);
            button16.Name = "button16";
            button16.Size = new Size(157, 58);
            button16.TabIndex = 27;
            button16.Text = "Room Management";
            button16.UseVisualStyleBackColor = false;
            button16.Click += button16_Click;
            // 
            // splitter2
            // 
            splitter2.BackColor = Color.MidnightBlue;
            splitter2.Dock = DockStyle.Top;
            splitter2.Location = new Point(0, 0);
            splitter2.Name = "splitter2";
            splitter2.Size = new Size(1237, 96);
            splitter2.TabIndex = 33;
            splitter2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.MidnightBlue;
            label2.Font = new Font("Noto Sans JP", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(863, 29);
            label2.Name = "label2";
            label2.Size = new Size(95, 26);
            label2.TabIndex = 34;
            label2.Text = "Welcome ";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.MidnightBlue;
            pictureBox3.BackgroundImage = Properties.Resources.Profile;
            pictureBox3.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox3.Location = new Point(814, 12);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(52, 50);
            pictureBox3.TabIndex = 35;
            pictureBox3.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(964, 31);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 37;
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Location = new Point(679, 159);
            panel1.Name = "panel1";
            panel1.Size = new Size(435, 247);
            panel1.TabIndex = 38;
            panel1.Paint += panel1_Paint;
            // 
            // Admin_dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1237, 684);
            Controls.Add(panel1);
            Controls.Add(textBox1);
            Controls.Add(button15);
            Controls.Add(pictureBox3);
            Controls.Add(label2);
            Controls.Add(button16);
            Controls.Add(button21);
            Controls.Add(button19);
            Controls.Add(button18);
            Controls.Add(User_manegement);
            Controls.Add(change_pw);
            Controls.Add(view_booking);
            Controls.Add(view_users);
            Controls.Add(splitter1);
            Controls.Add(label1);
            Controls.Add(splitter2);
            Name = "Admin_dashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form3";
            view_users.ResumeLayout(false);
            view_booking.ResumeLayout(false);
            change_pw.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Splitter splitter1;
        private Panel view_users;
        private Button button5;
        private Panel view_booking;
        private Button button8;
        private Panel change_pw;
        private Button button14;
        private Button button15;
        private Button User_manegement;
        private Button button18;
        private Button button19;
        private Button button21;
        private Button button16;
        private Splitter splitter2;
        private Label label2;
        private PictureBox pictureBox3;
        private TextBox textBox1;
        private Panel panel1;
    }
}