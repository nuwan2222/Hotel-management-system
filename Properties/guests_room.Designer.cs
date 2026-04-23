using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Hotel.Properties
{
    partial class guests_room
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
            dataGridView1 = new DataGridView();
            panel1 = new Panel();
            button2 = new Button();
            textBox6 = new TextBox();
            label13 = new Label();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            comboBox1 = new ComboBox();
            richTextBox1 = new RichTextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button1 = new Button();
            label10 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label11 = new Label();
            label12 = new Label();
            ((ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(49, 85);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(532, 489);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImageLayout = ImageLayout.Zoom;
            panel1.Controls.Add(button2);
            panel1.Controls.Add(textBox6);
            panel1.Controls.Add(label13);
            panel1.Controls.Add(dateTimePicker2);
            panel1.Controls.Add(dateTimePicker1);
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(richTextBox1);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label10);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(613, 85);
            panel1.Name = "panel1";
            panel1.Size = new Size(581, 489);
            panel1.TabIndex = 1;
            // 
            // button2
            // 
            button2.BackColor = Color.Red;
            button2.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(310, 433);
            button2.Name = "button2";
            button2.Size = new Size(94, 40);
            button2.TabIndex = 24;
            button2.Text = "back";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(165, 134);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(125, 27);
            textBox6.TabIndex = 23;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.White;
            label13.Location = new Point(19, 136);
            label13.Name = "label13";
            label13.Size = new Size(77, 21);
            label13.TabIndex = 22;
            label13.Text = "room_no";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(166, 249);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(242, 27);
            dateTimePicker2.TabIndex = 20;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(167, 213);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(241, 27);
            dateTimePicker1.TabIndex = 19;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Standard ", "Deluxe", "Superior", "Suite", "Premium", "Luxury", "Family" });
            comboBox1.Location = new Point(166, 167);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(109, 28);
            comboBox1.TabIndex = 18;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(167, 296);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(295, 112);
            richTextBox1.TabIndex = 17;
            richTextBox1.Text = "";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(165, 95);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(243, 27);
            textBox3.TabIndex = 14;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(165, 59);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(243, 27);
            textBox2.TabIndex = 13;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(165, 22);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(243, 27);
            textBox1.TabIndex = 12;
            // 
            // button1
            // 
            button1.BackColor = Color.LimeGreen;
            button1.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.White;
            button1.Location = new Point(165, 433);
            button1.Name = "button1";
            button1.Size = new Size(104, 40);
            button1.TabIndex = 10;
            button1.Text = "submit";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.White;
            label10.Location = new Point(3, 298);
            label10.Name = "label10";
            label10.Size = new Size(130, 21);
            label10.TabIndex = 9;
            label10.Text = "Additional info :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.White;
            label6.Location = new Point(19, 253);
            label6.Name = "label6";
            label6.Size = new Size(90, 21);
            label6.TabIndex = 5;
            label6.Text = "check out :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.White;
            label5.Location = new Point(19, 213);
            label5.Name = "label5";
            label5.Size = new Size(80, 21);
            label5.TabIndex = 4;
            label5.Text = "check in :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.White;
            label4.Location = new Point(19, 169);
            label4.Name = "label4";
            label4.Size = new Size(141, 21);
            label4.TabIndex = 3;
            label4.Text = "select room type :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.White;
            label3.Location = new Point(19, 101);
            label3.Name = "label3";
            label3.Size = new Size(88, 21);
            label3.TabIndex = 2;
            label3.Text = "phone no :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.White;
            label2.Location = new Point(19, 65);
            label2.Name = "label2";
            label2.Size = new Size(98, 21);
            label2.TabIndex = 1;
            label2.Text = "your email :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Noto Sans JP", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(19, 28);
            label1.Name = "label1";
            label1.Size = new Size(98, 21);
            label1.TabIndex = 0;
            label1.Text = "your name :";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Noto Sans JP", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.White;
            label11.Location = new Point(841, 33);
            label11.Name = "label11";
            label11.Size = new Size(149, 29);
            label11.TabIndex = 11;
            label11.Text = "Booking form";
            label11.Click += label11_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Noto Sans JP", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.White;
            label12.Location = new Point(185, 33);
            label12.Name = "label12";
            label12.Size = new Size(174, 29);
            label12.TabIndex = 2;
            label12.Text = "Available rooms";
            // 
            // guests_room
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MidnightBlue;
            BackgroundImage = Resources.dubai_2014317_1280;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1247, 661);
            Controls.Add(label12);
            Controls.Add(panel1);
            Controls.Add(dataGridView1);
            Controls.Add(label11);
            Name = "guests_room";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "guests_room";
            ((ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Panel panel1;
        private Label label11;
        private Button button1;
        private Label label10;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox comboBox1;
        private RichTextBox richTextBox1;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private DateTimePicker dateTimePicker1;
        private DateTimePicker dateTimePicker2;
        private Label label12;
        private TextBox textBox6;
        private Label label13;
        private Button button2;
    }
}