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
using Microsoft.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hotel.Properties
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //login button
        private void button1_Click(object sender, EventArgs e)
        {
            string Username = textBox1.Text.Trim();
            string Password = textBox2.Text.Trim();



            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Please enter both username and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT password , user_role FROM dbo.hotel WHERE username = @username";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", Username);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {



                        if (reader.Read())
                        {
                            string hashedPasswordFromDb = reader["password"].ToString();
                            string userRole = reader["user_role"].ToString();

                            if (signup.HASH.verifypassword(Password, hashedPasswordFromDb))
                            {
                                UserSession.CurrentUsername = Username;
                                UserSession.CurrentUserrole = userRole;

                                MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Open dashboard or next form here

                                if (userRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                                {
                                    Admin_dashboard admin = new Admin_dashboard();
                                    admin.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    Guest_s_dashboard guest = new Guest_s_dashboard();
                                    guest.Show();
                                    this.Hide();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Invalid password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Username not found", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //hash function
        public static class HASH
        {
            public static string Hashpassword(string password)
            {
                return BCrypt.Net.BCrypt.HashPassword(password);
            }

            public static bool Verifypassword(string inputpassword, string hashedpassword)
            {

                return BCrypt.Net.BCrypt.Verify(inputpassword, hashedpassword);
            }
        }

        //checkbox hide
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                textBox1.UseSystemPasswordChar = false;
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {

                textBox2.UseSystemPasswordChar = true;
            }
        }

        //signup form
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            signup signup = new signup();
            signup.Show();
            this.Hide();
        }
    }
}
