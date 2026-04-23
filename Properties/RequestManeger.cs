using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Resources;
using Microsoft.Data.SqlClient;

namespace Hotel.Properties
{
    public class RequestManeger
    {
        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        //create a list
        public List<Requests> GetAllRequests()
        {
            List<Requests> requests = new List<Requests>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.Requests";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    requests.Add(new Requests
                    {
                        GuestName = reader["guest_name"].ToString(),
                        RoomId = Convert.ToInt32(reader["room_id"]),
                        Description = reader["request_description"].ToString(),
                        Status = reader["status"].ToString(),
                        RequestDate = Convert.ToDateTime(reader["request_date"])

                    });
                }
            }

            return requests;
        }


            public List<Feedbacks> GetAllFeedbacks()
            {
                List<Feedbacks> feedbacks = new List<Feedbacks>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM dbo.Feedbacks";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        feedbacks.Add(new Feedbacks
                        {
                            FeedbackId = Convert.ToInt32(reader["feedback_id"]),
                            Username = reader["username"].ToString(),
                            Comments = reader["comments"].ToString(),
                            Star_ratings = Convert.ToInt32(reader["star_rating"]),
                            FeedbackDate = Convert.ToDateTime(reader["feedback_date"])
                        });
                    }
                }

                return feedbacks;
            }

        }
    }
