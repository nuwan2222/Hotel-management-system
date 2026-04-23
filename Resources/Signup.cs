using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotel.Properties;
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Hotel.Resources
{
    public partial class signup : Form
    {
        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";


        public signup()


        {
            InitializeComponent();
        }

        //login form
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        //sign up button
        private async void button1_Click(object sender, EventArgs e)
        {

            string Username = textBox1.Text.Trim();
            string Email = textBox2.Text.Trim();
            string Password = textBox3.Text.Trim();
            string ConfirmPassword = textBox4.Text.Trim();

            if (!Validateform())
                return;

            // Check for duplicate username or email
            if (!await CheckForDuplicates(Username, Email))
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string hashedpassword = HASH.Hashpassword(Password);

                    string query = "INSERT INTO dbo.hotel (username, password, email, user_role) VALUES (@username, @password, @email, @user_role)";
                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@username", Username);
                    cmd.Parameters.AddWithValue("@email", Email);
                    cmd.Parameters.AddWithValue("@password", hashedpassword);
                    cmd.Parameters.AddWithValue("@user_role", "Guest");

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear form fields
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //duplicate check function
       private async Task<bool> CheckForDuplicates(string username, string email)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Check for duplicate username
                    string usernameQuery = "SELECT COUNT(*) FROM dbo.hotel WHERE username = @username";
                    SqlCommand usernameCmd = new SqlCommand(usernameQuery, conn);
                    usernameCmd.Parameters.AddWithValue("@username", username);

                    conn.Open();
                    int usernameCount = (int)usernameCmd.ExecuteScalar();

                    if (usernameCount > 0)
                    {
                        MessageBox.Show("This username is already taken. Please choose a different username.",
                            "Username Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Focus();
                        textBox1.SelectAll();
                        return false;
                    }

                    // Check for duplicate email
                    string emailQuery = "SELECT COUNT(*) FROM dbo.hotel WHERE email = @email";
                    SqlCommand emailCmd = new SqlCommand(emailQuery, conn);
                    emailCmd.Parameters.AddWithValue("@email", email);

                    int emailCount = (int)emailCmd.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        MessageBox.Show("This email is already registered. Please use a different email address.",
                            "Email Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox2.Focus();
                        textBox2.SelectAll();
                        return false;
                    }

                    conn.Close();
                }

                return true; // No duplicate
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking for duplicates: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            } 
        }

        //HASH function
        public static class HASH{
             public static string Hashpassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            public static bool verifypassword(string inputpassword , string hashedpassword)
            {

                return BCrypt.Net.BCrypt.Verify(inputpassword, hashedpassword);
            }
        }
        
        //form validation
        private bool Validateform()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("username is requred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("email is required", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }

            if (!IsValidEmail(textBox2.Text))
            {
                MessageBox.Show("Please enter a vaild email address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Password is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
                return false;
            }

            if (textBox4.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 caracters long", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus();
                return false;
            }

            if (textBox4.Text != textBox3.Text)
            {
                MessageBox.Show("Password do not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox3.Focus();
                return false;
            }

            return true;

        }

        //validate email
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


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        //cancel button
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        //checkbox hide part
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox3.UseSystemPasswordChar = false;
                textBox4.UseSystemPasswordChar = false;
            }
            else
            {
                textBox3.UseSystemPasswordChar = true;
                textBox4.UseSystemPasswordChar = true;
            }
        }
    }
}
