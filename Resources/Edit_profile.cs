using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotel.Resources;
using Hotel.Properties;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Hotel.Resources
{
    public partial class Edit_profile : Form
    {
        private string currentUsername;

        //session handle
        public Edit_profile() : this(UserSession.CurrentUsername)
        {
          
        }

        public Edit_profile(string username)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("No user is currently logged in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            this.currentUsername = username;
            LoadCurrentUserData();
        }

        //get current details
        private void LoadCurrentUserData()
        {
            try
            {
          
                if (string.IsNullOrEmpty(currentUsername))
                {
                    MessageBox.Show("No user is currently logged in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

               
                string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT username, email FROM dbo.hotel WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", currentUsername);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        textBox1.Text = reader["username"].ToString();
                        textBox3.Text = reader["email"].ToString();
                    }
                    else
                    {
                        
                        MessageBox.Show("User not found in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Guest_s_dashboard guest_S_Dashboard = new Guest_s_dashboard();
            guest_S_Dashboard.Show();
            this.Hide();
        }

        //change button
        private void button1_Click(object sender, EventArgs e)
        {
            string newname = textBox2.Text.Trim();
            string newemail = textBox4.Text.Trim();
            string currentPassword = textBox5.Text.Trim();
            string newPassword = textBox6.Text.Trim();

            if (!ValidateForm())
                return;

            // Verify current password
            if (!VerifyCurrentPassword(currentPassword))
            {
                MessageBox.Show("Current password is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return;
            }

            try
            {
                UpdateUserProfile(newname, newemail, newPassword);
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Update current username if it was changed
                if (!string.IsNullOrEmpty(newname) && newname != currentUsername)
                {
                    currentUsername = newname;
                }

                // Refresh the form with new data
                LoadCurrentUserData();
                ClearNewDataFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating profile: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

        private void ClearNewDataFields()
        {
            textBox2.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private bool VerifyCurrentPassword(string currentPassword)
        {
            try
            {
               
                if (string.IsNullOrEmpty(currentUsername))
                {
                    MessageBox.Show("No user is currently logged in", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT password FROM dbo.hotel WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", currentUsername);

                    conn.Open();
                   
                    object result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        MessageBox.Show("User not found in database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    string hashedPassword = result.ToString();

                    if (string.IsNullOrEmpty(hashedPassword))
                        return false;

                    return HASH.verifypassword(currentPassword, hashedPassword);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error verifying password: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //update
        private void UpdateUserProfile(string newName, string newEmail, string newPassword)
        {
            string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Update username 
                if (!string.IsNullOrEmpty(newName) && newName != currentUsername)
                {
                    string updateUsernameQuery = "UPDATE dbo.hotel SET username = @newUsername WHERE username = @currentUsername";
                    SqlCommand cmd1 = new SqlCommand(updateUsernameQuery, conn);
                    cmd1.Parameters.AddWithValue("@newUsername", newName);
                    cmd1.Parameters.AddWithValue("@currentUsername", currentUsername);
                    cmd1.ExecuteNonQuery();
                }

                // Update email 
                if (!string.IsNullOrEmpty(newEmail))
                {
                    string updateEmailQuery = "UPDATE dbo.hotel SET email = @newEmail WHERE username = @username";
                    SqlCommand cmd2 = new SqlCommand(updateEmailQuery, conn);
                    cmd2.Parameters.AddWithValue("@newEmail", newEmail);
                    cmd2.Parameters.AddWithValue("@username", !string.IsNullOrEmpty(newName) ? newName : currentUsername);
                    cmd2.ExecuteNonQuery();
                }

                // Update password 
                if (!string.IsNullOrEmpty(newPassword))
                {
                    string hashedNewPassword = HASH.Hashpassword(newPassword);
                    string updatePasswordQuery = "UPDATE dbo.hotel SET password = @newPassword WHERE username = @username";
                    SqlCommand cmd3 = new SqlCommand(updatePasswordQuery, conn);
                    cmd3.Parameters.AddWithValue("@newPassword", hashedNewPassword);
                    cmd3.Parameters.AddWithValue("@username", !string.IsNullOrEmpty(newName) ? newName : currentUsername);
                    cmd3.ExecuteNonQuery();
                }
            }
        }

        //validate
        private bool ValidateForm()
        {
            
            if (string.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("Current password is required to make changes", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox5.Focus();
                return false;
            }

   
            if (!string.IsNullOrEmpty(textBox4.Text) && !IsValidEmail(textBox4.Text))
            {
                MessageBox.Show("Please enter a valid email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
                return false;
            }

           
            if (!string.IsNullOrEmpty(textBox6.Text) && textBox6.Text.Length < 8)
            {
                MessageBox.Show("New password must be at least 8 characters long", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox6.Focus();
                return false;
            }

            
            if (string.IsNullOrEmpty(textBox2.Text) &&
                string.IsNullOrEmpty(textBox4.Text) &&
                string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Please enter at least one field to update", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        //validate the email
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //hash function
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

    }


}
  