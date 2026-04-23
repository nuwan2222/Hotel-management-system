using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Hotel.Resources
{
    //admin room management
    public class ViewBookingManeger
    {
        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        //create list
        public List<ViewBooking> GetAllBooking()
        {
            List<ViewBooking> bookings = new List<ViewBooking>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.Bookings";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    bookings.Add(new ViewBooking
                    {
                        BookingId = Convert.ToInt32(reader["booking_id"]),
                        RoomNo = Convert.ToInt32(reader["room_no"]),
                        GuestName = reader["guest_name"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        CheckIn = Convert.ToDateTime(reader["check_in"]),
                        CheckOut = Convert.ToDateTime(reader["check_out"]),
                        AdditionalInfo =reader["additional_info"].ToString(),
                        BookingDate = Convert.ToDateTime(reader["booking_date"]),
                    });
                }
            }

            return bookings;
        }

    }
}
