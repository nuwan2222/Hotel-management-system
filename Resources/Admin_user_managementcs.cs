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
using Microsoft.VisualBasic.ApplicationServices;

namespace Hotel.Resources
{
    public partial class Admin_user_managementcs : Form
    {
        
        //create object
        UserManager userManager = new UserManager();

        //declare the connection
        string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        public Admin_user_managementcs()
        {
            InitializeComponent();
            LoadUsersIntoGrid();


        }

        // check duplicate user
        private bool  CheckForDuplicates(string username, string email, bool isEdit = false, string originalUsername = "")
        {
            try
            {
                using (Microsoft.Data.SqlClient.SqlConnection conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    conn.Open();

                    
                    string usernameQuery;
                    if (isEdit)
                    {
                        
                        usernameQuery = "SELECT COUNT(*) FROM dbo.hotel WHERE username = @username AND username != @originalUsername";
                    }
                    else
                    {
                        usernameQuery = "SELECT COUNT(*) FROM dbo.hotel WHERE username = @username";
                    }

                    Microsoft.Data.SqlClient.SqlCommand usernameCmd = new Microsoft.Data.SqlClient.SqlCommand(usernameQuery, conn);
                    usernameCmd.Parameters.AddWithValue("@username", username);
                    if (isEdit)
                        usernameCmd.Parameters.AddWithValue("@originalUsername", originalUsername);

                    int usernameCount = (int)usernameCmd.ExecuteScalar();

                    if (usernameCount > 0)
                    {
                        MessageBox.Show("This username is already taken. Please choose a different username.",
                            "Username Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.Focus();
                        textBox1.SelectAll();
                        return false;
                    }

                    
                    string emailQuery;
                    if (isEdit)
                    {
                        
                        emailQuery = "SELECT COUNT(*) FROM dbo.hotel WHERE email = @email AND username != @originalUsername";
                    }
                    else
                    {
                        emailQuery = "SELECT COUNT(*) FROM dbo.hotel WHERE email = @email";
                    }

                    Microsoft.Data.SqlClient.SqlCommand emailCmd = new Microsoft.Data.SqlClient.SqlCommand(emailQuery, conn);
                    emailCmd.Parameters.AddWithValue("@email", email);
                    if (isEdit)
                        emailCmd.Parameters.AddWithValue("@originalUsername", originalUsername);

                    int emailCount = (int)emailCmd.ExecuteScalar();

                    if (emailCount > 0)
                    {
                        MessageBox.Show("This email is already registered. Please use a different email address.",
                            "Email Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox3.Focus();
                        textBox3.SelectAll();
                        return false;
                    }

                    conn.Close();
                }

                return true; // No duplicates found
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error checking for duplicates: {ex.Message}",
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Add a field to store original username 
        private string originalUsername = "";

        //load table
        private void LoadUsersIntoGrid()
        {

            //session handlee
             if (!UserSession.IsLoggedIn)
              {
                 MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
              }

            List<Hotel.Properties.User> users = userManager.GetAllUsers();
            dataGridView1.DataSource = users;
        }

        //validate
        private bool ValidateInput()
        {
           
            
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return false;
            }
            
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a user role.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return false;
            }
            
            // Basic email validation
            if (!textBox3.Text.Contains("@") || !textBox3.Text.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return false;
            }
            

            return true;
        }

        private async void   button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                bool isEditMode = button1.Tag != null && button1.Tag.ToString() == "edit";

                // Check for duplicates
                // Check for duplicates 
                if (!CheckForDuplicates(textBox1.Text.Trim(), textBox3.Text.Trim(), isEditMode, originalUsername))
                    return; ;

                Hotel.Properties.User newUser = new Hotel.Properties.User
                {
                    Username = textBox1.Text.Trim(),
                    Password = textBox2.Text,
                    Email = textBox3.Text.Trim(),
                    User_role = comboBox1.SelectedItem.ToString()
                };

               
                if (isEditMode)
                {
                    
                    userManager.UpdateUser(newUser);

                    button1.Text = "Add";
                    button1.Tag = null;
                    originalUsername = ""; 
                }
                else
                {
                    
                    userManager.AddUser(newUser);
                    ClearForm();
                }
                LoadUsersIntoGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding user: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    

    
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected user
                Hotel.Properties.User selectedUser = (Hotel.Properties.User)dataGridView1.SelectedRows[0].DataBoundItem;

                originalUsername = selectedUser.Username;

            
                textBox1.Text = selectedUser.Username;
                textBox2.Text = ""; 
                textBox3.Text = selectedUser.Email;
                comboBox1.SelectedItem = selectedUser.User_role;

                // Change button text to indicate edit mode
                button1.Text = "Update";
                button1.Tag = "edit"; // 

                MessageBox.Show($"User '{selectedUser.Username}' loaded for editing. Leave password blank to keep current password.",
                               "Edit Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
            else
            {
                MessageBox.Show("Please select a user to edit.", "No Selection",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Hotel.Properties.User selectedUser = (Hotel.Properties.User)dataGridView1.SelectedRows[0].DataBoundItem;

                var confirm = MessageBox.Show($"Are you sure you want to delete user '{selectedUser.Username}'?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    userManager.DeleteUser(selectedUser.Username);
                    LoadUsersIntoGrid();
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void ClearForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
        }
    }
}
