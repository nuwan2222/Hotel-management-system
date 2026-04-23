using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Hotel.Resources
{

    //admin room management
    public class RoomManager
    {
        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        //create list
        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.Room";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    rooms.Add(new Room
                    {
                        Roomid = Convert.ToInt32(reader["room_id"]),
                        Roomno = Convert.ToInt32(reader["room_no"]),
                        Roomtype = reader["room_type"].ToString(),
                        Price = Convert.ToInt32(reader["price"]),
                        Status = reader["status"]?.ToString(),
                        CreatedDate = Convert.ToDateTime(reader["created_date"]),
                        UpdatedDate = Convert.ToDateTime(reader["updated_date"])
                    });
                }
            }

            return rooms;
        }

    }
}
