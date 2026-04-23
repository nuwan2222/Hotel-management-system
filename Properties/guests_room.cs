using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hotel.Resources;
using Microsoft.Data.SqlClient;

namespace Hotel.Properties
{
    public partial class guests_room : Form
    {
        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        public guests_room()
        {
            InitializeComponent();

            //session handling
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadRoomData();
        }

        //load
        private void LoadRoomData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT DISTINCT room_no, room_type, price, status FROM dbo.Room ORDER BY room_no";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading room data: {ex.Message}", "Database Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        // Method to refresh data
        public void RefreshData()
        {
            LoadRoomData();
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        //book
        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateBookingForm())
            {

                //get the room number from form
                string roomno = GetSelectedRoomNumber();

                if (!string.IsNullOrEmpty(roomno))
                {
                    if (ProcessBooking(roomno))
                    {
                        MessageBox.Show("Booking successful! Room has been booked.", "Booking Confirmed",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearBookingForm();

                        LoadRoomData();
                    }
                    else
                    {
                        MessageBox.Show("Please select a room number.", "Validation Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }


        }

        //form validation
        private bool ValidateBookingForm()
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
                MessageBox.Show("Please enter your email.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Please enter your phone number.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Focus();
                return false;
            }

            return true;
        }

        //get room number
        private string GetSelectedRoomNumber()
        {
            return textBox6.Text.Trim();
        }

        //status
        private bool ProcessBooking(string roomno)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            //check if room is still available
                            string checkQuery = "SELECT status FROM dbo.Room WHERE room_no = @roomNo";
                            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection, transaction))
                            {
                                checkCommand.Parameters.AddWithValue("@roomNo", roomno);
                                string currentStatus = checkCommand.ExecuteScalar()?.ToString();

                                if (currentStatus != "Available")
                                {
                                    MessageBox.Show("Sorry, this room is no longer available.", "Room Unavailable",
                                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return false;
                                }
                            }

                            // Insert booking record
                            string insertBookingQuery = @"
                                INSERT INTO dbo.Bookings (room_no, guest_name, email, phone, check_in, check_out, additional_info, booking_date)
                                VALUES (@roomNo, @guestName, @email, @phone, @checkIn, @checkOut, @additionalInfo, @bookingDate)";

                            using (SqlCommand insertCommand = new SqlCommand(insertBookingQuery, connection, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@roomNo", roomno);
                                insertCommand.Parameters.AddWithValue("@guestName", textBox1.Text.Trim());
                                insertCommand.Parameters.AddWithValue("@email", textBox2.Text.Trim());
                                insertCommand.Parameters.AddWithValue("@phone", textBox3.Text.Trim());
                                insertCommand.Parameters.AddWithValue("@checkIn", dateTimePicker1.Value);
                                insertCommand.Parameters.AddWithValue("@checkOut", dateTimePicker2.Value);
                                insertCommand.Parameters.AddWithValue("@additionalInfo", richTextBox1.Text.Trim());
                                insertCommand.Parameters.AddWithValue("@bookingDate", DateTime.Now);

                                insertCommand.ExecuteNonQuery();
                            }

                            // Update room status "Booked"
                            string updateRoomQuery = "UPDATE dbo.Room SET status = 'Booked' WHERE room_no = @roomNo";
                            using (SqlCommand updateCommand = new SqlCommand(updateRoomQuery, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@roomNo", roomno);
                                updateCommand.ExecuteNonQuery();
                            }

                          
                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing booking: {ex.Message}", "Booking Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        //clear form
        private void ClearBookingForm()
        {
            
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox6.Clear();
            richTextBox1.Clear();

            // Reset date  default values
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today.AddDays(1);

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Guest_s_dashboard guest_S_Dashboard = new Guest_s_dashboard();
            guest_S_Dashboard.Show();
            guest_S_Dashboard.RefreshBookingData();
            this.Hide();
        }
    }
}
