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
    public partial class guests_cancelbooking : Form
    {
        //create datagrid
        private DataGridView dataGridView1;
        private Label lblTitle;
        private Label lblNoBookings;
        private Button btnCancel;
        private Button btnBack;

        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        public guests_cancelbooking()
        {
            InitializeComponent();

            //session handle
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InitializeControls();
            this.Load += CancelBooking_Load;
        }

        //load
        private void CancelBooking_Load(object sender, EventArgs e)
        {
            LoadUserBookings();
        }

        //create table
        private void InitializeControls()
        {

            // Title Label
            lblTitle = new Label
            {
                Text = "Your Bookings",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(800, 40),
                Location = new Point(100, 50),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(lblTitle);

            // No Bookings Label
            lblNoBookings = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Red,
                BackColor = Color.Transparent,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(800, 60),
                Location = new Point(100, 420),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Visible = false
            };
            this.Controls.Add(lblNoBookings);

            // DataGridView
            dataGridView1 = new DataGridView
            {
                Location = new Point(100, 100),
                Size = new Size(800, 300),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dataGridView1);

            // Cancel Button
            btnCancel = new Button
            {
                Text = "Cancel Selected Booking",
                Size = new Size(180, 40),
                Location = new Point(250, 420),
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += button1_Click;
            this.Controls.Add(btnCancel);

            // Back Button
            btnBack = new Button
            {
                Text = "Back",
                Size = new Size(100, 40),
                Location = new Point(450, 420),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnBack.Click += button2_Click;
            this.Controls.Add(btnBack);
        }

        //loaduser
        private void LoadUserBookings()
        {
            try
            {
                string userEmail = GetCurrentUserEmail();
                if (string.IsNullOrEmpty(userEmail))
                {
                    ShowNoBookingsMessage("Error: Unable to get user information.");
                    return;
                }

                string query = @"
                    SELECT 
                        b.booking_id,
                        b.room_no,
                        b.guest_name,
                        b.email,
                        b.phone,
                        b.check_in,
                        b.check_out,
                        b.additional_info,
                        b.booking_date,
                        r.room_type,
                        r.price
                    FROM dbo.Bookings b
                    INNER JOIN dbo.room r ON b.room_no = r.room_no
                    WHERE b.email = @email
                    ORDER BY b.booking_date DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", userEmail);
                        connection.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            ShowNoBookingsMessage("You have no bookings to cancel.");
                            return;
                        }

                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Visible = true;
                        btnCancel.Visible = true;
                        lblNoBookings.Visible = false;

                        FormatColumns();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bookings: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowNoBookingsMessage("Error loading bookings. Please try again.");
            }
        }

        //get current email
        private string GetCurrentUserEmail()
        {
            if (string.IsNullOrEmpty(UserSession.CurrentUsername))
            {
                MessageBox.Show("User session expired. Please login again.", "Session Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            try
            {
                string query = "SELECT email FROM dbo.hotel WHERE username = @username";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", UserSession.CurrentUsername);
                        connection.Open();

                        return command.ExecuteScalar()?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting user email: {ex.Message}");
                return null;
            }
        }

        //show bookigs
        private void ShowNoBookingsMessage(string message)
        {
            lblNoBookings.Text = message;
            lblNoBookings.Visible = true;
            dataGridView1.Visible = false;
            btnCancel.Visible = false;
        }

        private void FormatColumns()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                // Hide booking_id column but keep it for reference
                if (dataGridView1.Columns.Contains("booking_id"))
                    dataGridView1.Columns["booking_id"].Visible = false;

                // Format column headers
                if (dataGridView1.Columns.Contains("room_no"))
                    dataGridView1.Columns["room_no"].HeaderText = "Room No";
                if (dataGridView1.Columns.Contains("guest_name"))
                    dataGridView1.Columns["guest_name"].HeaderText = "Guest Name";
                if (dataGridView1.Columns.Contains("email"))
                    dataGridView1.Columns["email"].HeaderText = "Email";
                if (dataGridView1.Columns.Contains("phone"))
                    dataGridView1.Columns["phone"].HeaderText = "Phone";
                if (dataGridView1.Columns.Contains("check_in"))
                    dataGridView1.Columns["check_in"].HeaderText = "Check In";
                if (dataGridView1.Columns.Contains("check_out"))
                    dataGridView1.Columns["check_out"].HeaderText = "Check Out";
                if (dataGridView1.Columns.Contains("additional_info"))
                    dataGridView1.Columns["additional_info"].HeaderText = "Additional Info";
                if (dataGridView1.Columns.Contains("booking_date"))
                    dataGridView1.Columns["booking_date"].HeaderText = "Booking Date";
                if (dataGridView1.Columns.Contains("room_type"))
                    dataGridView1.Columns["room_type"].HeaderText = "Room Type";
                if (dataGridView1.Columns.Contains("price"))
                    dataGridView1.Columns["price"].HeaderText = "Price";

              
                if (dataGridView1.Columns.Contains("booking_date"))
                    dataGridView1.Columns["booking_date"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                if (dataGridView1.Columns.Contains("check_in"))
                    dataGridView1.Columns["check_in"].DefaultCellStyle.Format = "dd/MM/yyyy";
                if (dataGridView1.Columns.Contains("check_out"))
                    dataGridView1.Columns["check_out"].DefaultCellStyle.Format = "dd/MM/yyyy";

                if (dataGridView1.Columns.Contains("price"))
                    dataGridView1.Columns["price"].DefaultCellStyle.Format = "C";
            }
        }

        //cancel button
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a booking to cancel.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            int bookingId = Convert.ToInt32(selectedRow.Cells["booking_id"].Value);
            int roomNo = Convert.ToInt32(selectedRow.Cells["room_no"].Value);
            string guestName = selectedRow.Cells["guest_name"].Value.ToString();
            DateTime checkIn = Convert.ToDateTime(selectedRow.Cells["check_in"].Value);

           
            if (checkIn < DateTime.Now.Date)
            {
                MessageBox.Show("Cannot cancel a booking that has already started or passed.",
                    "Cancellation Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to cancel the booking for {guestName}?\n" +
                $"Room: {roomNo}\nCheck-in: {checkIn:dd/MM/yyyy}",
                "Confirm Cancellation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                CancelBooking(bookingId, roomNo);
            }
        }

        private void CancelBooking(int bookingId, int roomNo)
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
                            // Delete  booking
                            string deleteBookingQuery = "DELETE FROM dbo.Bookings WHERE booking_id = @bookingId";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteBookingQuery, connection, transaction))
                            {
                                deleteCommand.Parameters.AddWithValue("@bookingId", bookingId);
                                deleteCommand.ExecuteNonQuery();
                            }

                            // Update room status to Available
                            string updateRoomQuery = "UPDATE dbo.room SET status = 'Available' WHERE room_no = @roomNo";
                            using (SqlCommand updateCommand = new SqlCommand(updateRoomQuery, connection, transaction))
                            {
                                updateCommand.Parameters.AddWithValue("@roomNo", roomNo);
                                updateCommand.ExecuteNonQuery();
                            }

                           
                            transaction.Commit();

                            MessageBox.Show("Booking cancelled successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Refresh the bookings list
                            LoadUserBookings();
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
                MessageBox.Show($"Error cancelling booking: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void RefreshData()
        {
            LoadUserBookings();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void guests_cancelbooking_Load(object sender, EventArgs e)
        {

        }
    }
}
