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


namespace Hotel.Resources
{
    public partial class Guest_s_dashboard : Form
    {
        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";
        
        //username
        private string currentUsername;
        public Guest_s_dashboard()
        {

            InitializeComponent();

            // Set welcome message with username session
            if (UserSession.IsLoggedIn)
            {

                currentUsername = UserSession.CurrentUsername;

                if (string.IsNullOrEmpty(currentUsername))
                {
                    MessageBox.Show("Session username is empty. Please login again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                textBox1.Text = UserSession.CurrentUsername; //  welcome textbox
                textBox1.ReadOnly = true;
            }
            else
            {
                textBox1.Text = "Guest";
            }


            no_book.Size = new Size(400, 400);
            no_book.Location = new Point(250, 150);

            LoadBookingDetails();
            LoadBookingForCurrentUser();

        }

        //refresh
        public void RefreshBookingData()
        {
            LoadBookingForCurrentUser();
        }

        private void LoadBookingForCurrentUser()
        {
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Please login to see your bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT room_no, check_in, check_out, booking_date
                        FROM dbo.Bookings
                        WHERE guest_name = @username
                        ORDER BY booking_date DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", currentUsername);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                List<string> roomNumbers = new List<string>();
                                List<string> checkIns = new List<string>();
                                List<string> checkOuts = new List<string>();
                                List<string> bookingDates = new List<string>();

                                while (reader.Read())
                                {
                                    roomNumbers.Add(reader["room_no"].ToString());
                                    checkIns.Add(Convert.ToDateTime(reader["check_in"]).ToShortDateString());
                                    checkOuts.Add(Convert.ToDateTime(reader["check_out"]).ToShortDateString());
                                    bookingDates.Add(Convert.ToDateTime(reader["booking_date"]).ToString("g"));
                                }

                                // Populate labels with booking data
                                label7.Text = $"Room no : {string.Join(", ", roomNumbers)}";
                                label8.Text = $"Check-in : {string.Join(", ", checkIns)}";
                                label9.Text = $"Check-out : {string.Join(", ", checkOuts)}";
                                //label10.Text = $"Booking date : {string.Join(", ", bookingDates)}";

                                no_book.Visible = false;
                                ShowBookingDetails(true);
                            }
                            else
                            {
                                no_book.Visible = true;
                                ShowBookingDetails(false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load booking data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                no_book.Visible = true;
                ShowBookingDetails(false);
            }
        }




        private void LoadBookingDetails()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    
                    string query = "SELECT room_no, room_type, price FROM dbo.Room WHERE status = 'Booked' ORDER BY room_no";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            List<string> bookedRooms = new List<string>();
                            List<string> roomTypes = new List<string>();
                            List<string> prices = new List<string>();

                           
                            while (reader.Read())
                            {
                                bookedRooms.Add(reader["room_no"].ToString());
                                roomTypes.Add(reader["room_type"].ToString());
                                prices.Add(reader["price"].ToString());
                            }

                            if (bookedRooms.Count > 0)
                            {

                                if (bookedRooms.Count == 1)
                                {
                               
                                    label7.Text = $"Room no : {bookedRooms[0]}";
                                    label8.Text = $"Room type : {roomTypes[0]}";
                                }
                                else
                                {
                                  
                                    label7.Text = $"Room no : {string.Join(", ", bookedRooms)}";
                                    label8.Text = $"Room type : {string.Join(", ", roomTypes)}";
                                }

                                try
                                {
                                    foreach (Control ctrl in this.Controls)
                                    {
                                        if (ctrl is Label && ctrl.Text.Contains("Price"))
                                        {
                                            if (prices.Count == 1)
                                            {
                                                ctrl.Text = $"Price : ${prices[0]}";
                                            }
                                            else
                                            {
                                                ctrl.Text = $"Price : ${string.Join(", $", prices)}";
                                            }
                                            break;
                                        }
                                        
                                        if (ctrl.HasChildren)
                                        {
                                            foreach (Control subCtrl in ctrl.Controls)
                                            {
                                                if (subCtrl is Label && subCtrl.Text.Contains("Price"))
                                                {
                                                    if (prices.Count == 1)
                                                    {
                                                        subCtrl.Text = $"Price : ${prices[0]}";
                                                    }
                                                    else
                                                    {
                                                        subCtrl.Text = $"Price : ${string.Join(", $", prices)}";
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    
                                }

                                // Hide no booking picture 
                                no_book.Visible = false;
                                ShowBookingDetails(true);
                            }
                            else
                            {
                                // No bookings found - show no booking picture
                                no_book.Visible = true;
                                ShowBookingDetails(false);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading booking: {ex.Message}", "Database Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                no_book.Visible = true;
                ShowBookingDetails(false);
            }
        }

        
        //show bookings

        private void ShowBookingDetails(bool show)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel)
                {
                    bool hasBookingLabels = false;
                    foreach (Control subControl in panel.Controls)
                    {
                        if (subControl is Label label &&
                            (label.Text.Contains("Room no :") ||
                             label.Text.Contains("Check-in :") ||
                             label.Text.Contains("Check-out :") ||
                             label.Text.Contains("Booking date :")))
                        {
                            hasBookingLabels = true;
                            break;
                        }
                    }

                    if (hasBookingLabels)
                    {
                        panel.Visible = show;
                        break;
                    }
                }
            }
        }




        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //logout button
        private void button1_Click(object sender, EventArgs e)
        {
            UserSession.Clearsession();
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            guests_feedback guests_Feedback = new guests_feedback();
            guests_Feedback.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if user is logged in
                if (!UserSession.IsLoggedIn)
                {
                    MessageBox.Show("Please login first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create and show Edit_profile form
                Edit_profile profile = new Edit_profile(UserSession.CurrentUsername);
                profile.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            guests_history History = new guests_history();
            History.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            guests_cancelbooking guests_Cancelbooking = new guests_cancelbooking();
            guests_Cancelbooking.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Facilities facilities = new Facilities();
            facilities.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            guests_requests Requests = new guests_requests();
            Requests.Show();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            guests_room Room = new guests_room();
            Room.Show();
        }

        private void splitter3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            RefreshBookingData();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            gallry gallry = new gallry();   
            gallry.Show();
        }
    }
}
