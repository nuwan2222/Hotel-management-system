using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotel.Properties;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hotel.Resources
{
    public partial class guests_feedback : Form
    {
        private int selectedStarRating = 0;
        private PictureBox[] starPictureBoxes;

        public guests_feedback()
        {
            InitializeComponent();

            //session handle
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            initializeStarRatings();  
            LoadCurrentUser();
        }

        // star box 5
        private void initializeStarRatings()
        {
            starPictureBoxes = new PictureBox[]
            {
                pictureBox1,
                pictureBox2,
                pictureBox3,
                pictureBox4,
                pictureBox5
            };

            for (int i = 0; i < starPictureBoxes.Length; i++)
            {
                int starIndex = i + 1;
                starPictureBoxes[i].Click += (sender, e) => SetStarRating(starIndex);
                starPictureBoxes[i].Cursor = Cursors.Hand;
            }
            UpdateStarDisplay();

        }

        //load user
        private void LoadCurrentUser()
        {
            if (UserSession.IsLoggedIn)
            {
                textBox1.Text = UserSession.CurrentUsername;
                textBox1.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Please login first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void SetStarRating(int rating)
        {
            selectedStarRating = rating;
            UpdateStarDisplay();
        }

        private void UpdateStarDisplay()
        {
            for (int i = 0; i < starPictureBoxes.Length; i++)
            {
                if (i < selectedStarRating)
                {
                    // Show filled star (you need to have star images)
                    starPictureBoxes[i].BackColor = Color.Gold; 
                }
                else
                {
                    // Show empty star
                    starPictureBoxes[i].BackColor = Color.LightGray; 
                }
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        //submit button
        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string comments = richTextBox1.Text.Trim(); 

            // Validation
            if (selectedStarRating == 0)
            {
                MessageBox.Show("Please select a star rating", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Username is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(comments))
            {
                MessageBox.Show("Please enter your comments", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                richTextBox1.Focus();
                return;
            }

            try
            {
                SaveFeedbackToDatabase(username, comments, selectedStarRating);
                MessageBox.Show("Thank you for your feedback!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

               
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving feedback: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            // Reset star rating
            selectedStarRating = 0;
            UpdateStarDisplay();

            // Clear comments
            richTextBox1.Clear();
            richTextBox1.Focus(); // Set focus to comments box

          
        }
        
          //inset to the table
             private void SaveFeedbackToDatabase(string username, string comments, int starRating)
        {
            string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO Feedbacks (username, comments, star_rating, feedback_date) 
                               VALUES (@username, @comments, @star_rating, @feedback_date)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@comments", comments);
                cmd.Parameters.AddWithValue("@star_rating", starRating);
                cmd.Parameters.AddWithValue("@feedback_date", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    }
