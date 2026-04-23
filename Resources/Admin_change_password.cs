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

namespace Hotel.Resources
{
    public partial class Admin_change_password : Form
    {
        //create object from Change_PaaswordManager
        Change_PaaswordManager changepass = new Change_PaaswordManager();

        //declare the connection
        string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        public Admin_change_password()
        {
            InitializeComponent();
            LoadpassIntoGrid();
        }

        //change password function
        private void ChangePassword()
        {
            try
            {
                
                string username = textBox1.Text.Trim();
                string currentPassword = textBox2.Text;
                string newPassword = textBox3.Text;

             
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter a username.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(currentPassword))
                {
                    MessageBox.Show("Please enter the current password.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(newPassword))
                {
                    MessageBox.Show("Please enter a new password.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.Focus();
                    return;
                }

                
                if (newPassword.Length < 8)
                {
                    MessageBox.Show("New password must be at least 8 characters long.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.Focus();
                    return;
                }

                
                if (currentPassword == newPassword)
                {
                    MessageBox.Show("New password must be different from current password.",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.Focus();
                    return;
                }

                
                if (!VerifyCurrentPassword(username, currentPassword))
                {
                    MessageBox.Show("Current password is incorrect.", "Authentication Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Focus();
                    return;
                }

              
                bool success = ChangeUserPassword(username, newPassword);

                if (success)
                {
                    MessageBox.Show("Password changed successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearForm();

                    // Reload 
                    LoadpassIntoGrid();
                }
                else
                {
                    MessageBox.Show("Failed to change password. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //get current password
            private bool VerifyCurrentPassword(string username, string currentPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT password FROM dbo.hotel WHERE username = @username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        connection.Open();

                        string storedHashedPassword = command.ExecuteScalar()?.ToString();

                        if (string.IsNullOrEmpty(storedHashedPassword))
                        {
                            return false; // User not found
                        }

                        // Use BCrypt to verify password
                        return HASH.verifypassword(currentPassword, storedHashedPassword);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Method to change user password using BCrypt hashing
        private bool ChangeUserPassword(string username, string newPassword)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Hash  new password
                    string hashedNewPassword = HASH.Hashpassword(newPassword);

                    string query = "UPDATE dbo.hotel SET password = @newPassword WHERE username = @username";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newPassword", hashedNewPassword);
                        command.Parameters.AddWithValue("@username", username);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // hash function
        public static class HASH
        {
            public static string Hashpassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            public static bool verifypassword(string inputpassword, string hashedpassword)
            {
                return BCrypt.Net.BCrypt.Verify(inputpassword, hashedpassword);
            }
        }

        private void ClearForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
        }

   
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                // Auto-fill username from selected row
                textBox1.Text = selectedRow.Cells["username"].Value?.ToString() ?? "";
            }
        }

        //load
        private void LoadpassIntoGrid()
        {

           
             if (!UserSession.IsLoggedIn)
             {
                 MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
             }

            List<Change_password> req = changepass.GetAllpass();
            dataGridView1.DataSource = req;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChangePassword();
        }
    }
}
