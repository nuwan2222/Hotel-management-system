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
    public partial class Admin_room_management : Form
    {
        //declare the connection
        string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        //crete object from RoomManager
        RoomManager roomManager = new RoomManager();

        public Admin_room_management()
        {
            InitializeComponent();

            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.SelectionChanged += new EventHandler(this.dataGridView1_SelectionChanged);

            LoadRoomsIntoGrid();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                
                try
                {
                    if (dataGridView1.Columns.Contains("room_id") && row.Cells["room_id"].Value != null)
                    {
                        selectedRoomId = Convert.ToInt32(row.Cells["room_id"].Value);
                    }
                    else if (row.Cells.Count > 0 && row.Cells[0].Value != null)
                    {
                        selectedRoomId = Convert.ToInt32(row.Cells[0].Value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting room ID: " + ex.Message);
                }
            }
        }

        private void LoadRoomsIntoGrid()
        {

            //manage session
            if (!UserSession.IsLoggedIn)
          {
                MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               return;
            }

            List<Room> rooms = roomManager.GetAllRooms();
            dataGridView1.DataSource = rooms;


        }

        private int selectedRoomId = -1;
        private int selectedRowIndex = -1;

        //add input boxes
        private void button1_Click(object sender, EventArgs e)
        {
            string roomNo = Microsoft.VisualBasic.Interaction.InputBox("Enter Room Number:", "Add Room", "");
            if (string.IsNullOrEmpty(roomNo)) return;

            string roomType = Microsoft.VisualBasic.Interaction.InputBox("Enter Room Type (Standard/Deluxe/Suite):", "Add Room", "Standard");
            if (string.IsNullOrEmpty(roomType)) return;

            string priceStr = Microsoft.VisualBasic.Interaction.InputBox("Enter Price:", "Add Room", "100.00");
            if (string.IsNullOrEmpty(priceStr)) return;

            string status = Microsoft.VisualBasic.Interaction.InputBox("Enter Status (Available/Booked):", "Add Room", "Available");
            if (string.IsNullOrEmpty(status)) return;

            if (!decimal.TryParse(priceStr, out decimal price))
            {
                MessageBox.Show("Invalid price format!");
                return;
            }

            if (!int.TryParse(roomNo, out int roomNumber))
            {
                MessageBox.Show("Invalid room number format!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO dbo.Room (room_no, room_type, price, status, created_date, updated_date)
                                VALUES (@room_no, @room_type, @price, @status, @created_date, @updated_date)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@room_no", roomNumber);
                    cmd.Parameters.AddWithValue("@room_type", roomType);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@created_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@updated_date", DateTime.Now);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Room added successfully!");
                        LoadRoomsIntoGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error adding room: " + ex.Message);
                    }
                }
            }

        }

      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                selectedRowIndex = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Check available column names 
                if (row.Cells["room_id"] != null && row.Cells["room_id"].Value != null)
                {
                    selectedRoomId = Convert.ToInt32(row.Cells["room_id"].Value);
                }
                else if (dataGridView1.Columns.Contains("room_id"))
                {
                    selectedRoomId = Convert.ToInt32(row.Cells[0].Value); 
                }

                string columns = "";
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    columns += col.Name + ", ";
                }
                MessageBox.Show($"Available columns: {columns}\nSelected Row: {selectedRowIndex}\nRoom ID: {selectedRoomId}");
            }
        }

        //delete
        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex == -1 && dataGridView1.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                try
                {
                    if (dataGridView1.Columns.Contains("room_id"))
                    {
                        selectedRoomId = Convert.ToInt32(row.Cells["room_id"].Value);
                    }
                    else if (row.Cells.Count > 0)
                    {
                        selectedRoomId = Convert.ToInt32(row.Cells[0].Value); 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting room data: " + ex.Message);
                    return;
                }
            }

            if (selectedRowIndex == -1)
            {
                MessageBox.Show("Please select a room to delete.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this room?",
                                                "Confirm Delete",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM dbo.Room WHERE room_id = @room_id";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@room_id", selectedRoomId);

                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Room deleted successfully!");
                            LoadRoomsIntoGrid();
                            selectedRoomId = -1;
                            selectedRowIndex = -1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error deleting room: " + ex.Message);
                        }
                    }
                }
            }
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (selectedRowIndex == -1 && dataGridView1.SelectedRows.Count > 0)
            {
                selectedRowIndex = dataGridView1.SelectedRows[0].Index;
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                try
                {
                    if (dataGridView1.Columns.Contains("room_id"))
                    {
                        selectedRoomId = Convert.ToInt32(selectedRow.Cells["room_id"].Value);
                    }
                    else if (selectedRow.Cells.Count > 0)
                    {
                        selectedRoomId = Convert.ToInt32(selectedRow.Cells[0].Value);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error getting room data: " + ex.Message);
                    return;
                }
            }

            if (selectedRowIndex == -1)
            {
                MessageBox.Show("Please select a room to edit.");
                return;
            }

            DataGridViewRow row = dataGridView1.Rows[selectedRowIndex];

            // Get current values using column index 
            string currentRoomNo = "";
            string currentRoomType = "";
            string currentPrice = "";
            string currentStatus = "";

            try
            {
                // Try by column name first, then by index
                if (dataGridView1.Columns.Contains("room_no"))
                    currentRoomNo = row.Cells["room_no"].Value?.ToString() ?? "";
                else if (row.Cells.Count > 1)
                    currentRoomNo = row.Cells[1].Value?.ToString() ?? "";

                if (dataGridView1.Columns.Contains("room_type"))
                    currentRoomType = row.Cells["room_type"].Value?.ToString() ?? "";
                else if (row.Cells.Count > 2)
                    currentRoomType = row.Cells[2].Value?.ToString() ?? "";

                if (dataGridView1.Columns.Contains("price"))
                    currentPrice = row.Cells["price"].Value?.ToString() ?? "";
                else if (row.Cells.Count > 3)
                    currentPrice = row.Cells[3].Value?.ToString() ?? "";

                if (dataGridView1.Columns.Contains("status"))
                    currentStatus = row.Cells["status"].Value?.ToString() ?? "";
                else if (row.Cells.Count > 4)
                    currentStatus = row.Cells[4].Value?.ToString() ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading current values: " + ex.Message);
                return;
            }

            // Get new values from user
            string roomNo = Microsoft.VisualBasic.Interaction.InputBox("Enter Room Number:", "Edit Room", currentRoomNo);
            if (string.IsNullOrEmpty(roomNo)) return;

            string roomType = Microsoft.VisualBasic.Interaction.InputBox("Enter Room Type (Standard/Deluxe/Suite):", "Edit Room", currentRoomType);
            if (string.IsNullOrEmpty(roomType)) return;

            string priceStr = Microsoft.VisualBasic.Interaction.InputBox("Enter Price:", "Edit Room", currentPrice);
            if (string.IsNullOrEmpty(priceStr)) return;

            string status = Microsoft.VisualBasic.Interaction.InputBox("Enter Status (Available/Booked):", "Edit Room", currentStatus);
            if (string.IsNullOrEmpty(status)) return;

            if (!decimal.TryParse(priceStr, out decimal price))
            {
                MessageBox.Show("Invalid price format!");
                return;
            }

            if (!int.TryParse(roomNo, out int roomNumber))
            {
                MessageBox.Show("Invalid room number format!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"UPDATE dbo.Room SET 
                                room_no = @room_no,
                                room_type = @room_type,
                                price = @price,
                                status = @status,
                                updated_date = @updated_date
                                WHERE room_id = @room_id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@room_no", roomNumber);
                    cmd.Parameters.AddWithValue("@room_type", roomType);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@updated_date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@room_id", selectedRoomId);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Room updated successfully!");
                        LoadRoomsIntoGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating room: " + ex.Message);
                    }
                }
            }
        }
    }
}
    

