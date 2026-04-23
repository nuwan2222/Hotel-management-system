namespace Hotel.Resources
{
    partial class guests_requests
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Noto Sans JP", 13.7999992F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(317, 51);
            label1.Name = "label1";
            label1.Size = new Size(118, 34);
            label1.TabIndex = 19;
            label1.Text = "Requests";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(244, 138);
            label2.Name = "label2";
            label2.Size = new Size(60, 21);
            label2.TabIndex = 20;
            label2.Text = "name :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(244, 196);
            label3.Name = "label3";
            label3.Size = new Size(75, 21);
            label3.TabIndex = 21;
            label3.Text = "room id :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(244, 250);
            label4.Name = "label4";
            label4.Size = new Size(76, 21);
            label4.TabIndex = 22;
            label4.Text = "request :";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(341, 140);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 23;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(341, 194);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(125, 27);
            textBox2.TabIndex = 24;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(341, 250);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(184, 61);
            richTextBox1.TabIndex = 25;
            richTextBox1.Text = "";
            // 
            // button1
            // 
            button1.BackColor = Color.LimeGreen;
            button1.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(341, 347);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 26;
            button1.Text = "submit";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveBorder;
            button2.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(451, 347);
            button2.Name = "button2";
            button2.Size = new Size(74, 29);
            button2.TabIndex = 27;
            button2.Text = "back";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // guests_requests
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MidnightBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "guests_requests";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "guests_requests";
            Load += guests_requests_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;
        private RichTextBox richTextBox1;
        private Button button1;
        private Button button2;
    }
}