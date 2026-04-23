namespace Hotel.Resources
{
    partial class Facilities
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Facilities));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Noto Sans JP", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(247, 45);
            label1.Name = "label1";
            label1.Size = new Size(202, 34);
            label1.TabIndex = 0;
            label1.Text = " About Our Hotel";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.ForeColor = Color.White;
            label2.Location = new Point(22, 96);
            label2.Name = "label2";
            label2.Size = new Size(475, 120);
            label2.TabIndex = 1;
            label2.Text = resources.GetString("label2.Text");
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Noto Sans JP", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(262, 244);
            label3.Name = "label3";
            label3.Size = new Size(163, 34);
            label3.TabIndex = 2;
            label3.Text = "Our Facilities";
            // 
            // label4
            // 
            label4.ForeColor = Color.White;
            label4.Location = new Point(22, 301);
            label4.Name = "label4";
            label4.Size = new Size(492, 124);
            label4.TabIndex = 3;
            label4.Text = resources.GetString("label4.Text");
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.facilities;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(520, 28);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(233, 171);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = Properties.Resources.Pool_at_Beach_Hotel_03_scaled;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox2.Location = new Point(520, 264);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(233, 147);
            pictureBox2.TabIndex = 5;
            pictureBox2.TabStop = false;
            // 
            // Facilities
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MidnightBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Facilities";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Facilities";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}