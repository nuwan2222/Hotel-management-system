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
    public partial class guests_history : Form
    {
        //create datagrid
        private DataGridView dataGridView1;
        private Label lblHistory;
        private Label lblNoHistory;

        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        public guests_history()
        {
            InitializeComponent();

            //session handling
            if (!UserSession.IsLoggedIn)
            {
                MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InitializeDataGridView();
            this.Load += Guests_history_Load;
        }

        private void Guests_history_Load(object sender, EventArgs e)
        {
            LoadGuestHistory();
        }

        private void InitializeDataGridView()
        {
            // Set form properties
            this.Text = "My Booking History";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Create History Label
            lblHistory = new Label();
            lblHistory.Text = "My Booking History";
            lblHistory.Font = new Font("Noto Sans JP", 14, FontStyle.Bold);
            lblHistory.ForeColor = Color.White;
            lblHistory.BackColor = Color.Transparent;
            lblHistory.TextAlign = ContentAlignment.MiddleCenter;
            lblHistory.Location = new Point(0, 20);
            lblHistory.Size = new Size(this.Width, 50);
            lblHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Create "No History" Label
            lblNoHistory = new Label();
            lblNoHistory.Text = "No booking history found.\nYou haven't made any bookings yet.";
            lblNoHistory.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            lblNoHistory.ForeColor = Color.White;
            lblNoHistory.BackColor = Color.Transparent;
            lblNoHistory.TextAlign = ContentAlignment.MiddleCenter;
            lblNoHistory.Location = new Point(0, 200);
            lblNoHistory.Size = new Size(this.Width, 100);
            lblNoHistory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblNoHistory.Visible = false; 

            // Create DataGridView (smaller and centered)
            dataGridView1 = new DataGridView();
            dataGridView1.Location = new Point(100, 90);
            dataGridView1.Size = new Size(800, 350);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Create Back Button
            Button btnBack = new Button();
            btnBack.Text = "Back";
            btnBack.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnBack.BackColor = Color.Gray;
            btnBack.ForeColor = Color.White;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Size = new Size(100, 35);
            btnBack.Location = new Point(450, 460);
            btnBack.Cursor = Cursors.Hand;
            btnBack.Click += (s, e) => this.Close();

            // Add controls to form
            this.Controls.Add(lblHistory);
            this.Controls.Add(lblNoHistory);
            this.Controls.Add(dataGridView1);
            this.Controls.Add(btnBack);
        }

        //load
        private void LoadGuestHistory()
        {
            try
            {
               
                using (SqlConnection testConnection = new SqlConnection(connectionString))
                {
                    testConnection.Open();
                    testConnection.Close();
                }

                // Get the logged-in user's email/username 
                string currentUserEmail = GetCurrentUserEmail();

                if (string.IsNullOrEmpty(currentUserEmail))
                {
                    ShowNoHistoryMessage("Unable to retrieve user information.");
                    return;
                }

                //  filter by current user's email
                string query = @"
                    SELECT 
                        booking_id,
                        room_no,
                        guest_name,
                        email,
                        phone,
                        check_in,
                        check_out,
                        additional_info,
                        booking_date
                    FROM Bookings 
                    WHERE email = @userEmail 
                    ORDER BY booking_date DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@userEmail", currentUserEmail);

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count == 0)
                    {
                        
                        ShowNoHistoryMessage("No booking history found.\nYou haven't made any bookings yet.");
                    }
                    else
                    {
                        
                        dataGridView1.DataSource = dataTable;
                        dataGridView1.Visible = true;
                        lblNoHistory.Visible = false;

                        FormatColumns();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading guest history: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //get email
        private string GetCurrentUserEmail()
        {
            try
            {
                
                string query = "SELECT email FROM dbo.hotel WHERE username = @username";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", UserSession.CurrentUsername);
                        connection.Open();

                        string email = command.ExecuteScalar()?.ToString();
                        return email;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting user information: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        //no history message
        private void ShowNoHistoryMessage(string message)
        {
            lblNoHistory.Text = message;
            lblNoHistory.Visible = true;
            dataGridView1.Visible = false;
        }


        private void FormatColumns()
        {
            if (dataGridView1.Columns.Count > 0)
            {
                // Set column headers
                dataGridView1.Columns["booking_id"].HeaderText = "Booking ID";
                dataGridView1.Columns["room_no"].HeaderText = "Room No";
                dataGridView1.Columns["guest_name"].HeaderText = "Guest Name";
                dataGridView1.Columns["email"].HeaderText = "Email";
                dataGridView1.Columns["phone"].HeaderText = "Phone";
                dataGridView1.Columns["check_in"].HeaderText = "Check In";
                dataGridView1.Columns["check_out"].HeaderText = "Check Out";
                dataGridView1.Columns["additional_info"].HeaderText = "Additional Info";
                dataGridView1.Columns["booking_date"].HeaderText = "Booking Date";

                // Set column widths
                dataGridView1.Columns["booking_id"].Width = 80;
                dataGridView1.Columns["room_no"].Width = 80;
                dataGridView1.Columns["guest_name"].Width = 120;
                dataGridView1.Columns["email"].Width = 150;
                dataGridView1.Columns["phone"].Width = 100;
                dataGridView1.Columns["check_in"].Width = 100;
                dataGridView1.Columns["check_out"].Width = 100;
                dataGridView1.Columns["additional_info"].Width = 150;
                dataGridView1.Columns["booking_date"].Width = 120;

                // Format date columns
                dataGridView1.Columns["check_in"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns["check_out"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.Columns["booking_date"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

               
                dataGridView1.Columns["email"].Visible = false;
            }
        }

        }
}
