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
    public partial class guests_requests : Form
    {
        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        public guests_requests()
        {
            InitializeComponent();

            //session handle
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void guests_requests_Load(object sender, EventArgs e)
        {

        }

        //validate form
        private bool ValidateRequestForm()
        {

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please enter your name.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }


            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Please enter your room ID.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }


            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Please enter your request.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richTextBox1.Focus();
                return false;
            }

            if (!IsValidRoomId(textBox2.Text.Trim()))
            {
                MessageBox.Show("Invalid room ID or room is not booked.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }

            return true;
        }

        //check room
        private bool IsValidRoomId(string roomId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM dbo.Room WHERE room_no = @roomId AND status = 'Booked'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomId", roomId);
                        int count = (int)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error validating room ID: {ex.Message}", "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

        }

        //save the request
        private bool SaveRequest()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();


                    string insertQuery = @"
                        INSERT INTO dbo.Requests (guest_name, room_id, request_description, request_date, status)
                        VALUES (@guestName, @roomId, @requestDescription, @requestDate, @status)";

                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {

                        command.Parameters.AddWithValue("@guestName", textBox1.Text.Trim());
                        command.Parameters.AddWithValue("@roomId", textBox2.Text.Trim());
                        command.Parameters.AddWithValue("@requestDescription", richTextBox1.Text.Trim());
                        command.Parameters.AddWithValue("@requestDate", DateTime.Now);
                        command.Parameters.AddWithValue("@status", "Pending"); // Default status

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving request: {ex.Message}", "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //submit button
        private void button1_Click_1(object sender, EventArgs e)
        {

            if (ValidateRequestForm())
            {

                if (SaveRequest())
                {
                    MessageBox.Show("Request submitted successfully!", "Request Submitted",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
