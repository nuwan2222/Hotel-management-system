using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Properties;
using Microsoft.Data.SqlClient;

namespace Hotel.Resources
{


    public class Change_PaaswordManager
    {
        //declare the connection
        private string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";

        public List<Change_password> GetAllpass()
        {
            List<Change_password> change = new List<Change_password>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM dbo.hotel";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    change.Add(new Change_password
                    {
                        UserName = reader["username"].ToString(),
                        Password = reader["password"].ToString(),
                        Email = reader["email"].ToString(),
                        User_role = reader["user_role"].ToString()

                    });
                }
            }

            return change;
        }
    }
}

    
