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

namespace Hotel.Resources
{
    public partial class Admin_view_Booking : Form
    {
        //create object from ViewBookingManeger
        ViewBookingManeger ViewBooking = new ViewBookingManeger();

        public Admin_view_Booking()
        {
            InitializeComponent();
            LoadBookingsIntoGrid();

        }

        //load
        private void LoadBookingsIntoGrid()
        {

            //session management
             if (!UserSession.IsLoggedIn)
              {
                  MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                 return;
              }

             //get list
            List<ViewBooking> rooms = ViewBooking.GetAllBooking();
            dataGridView1.DataSource = rooms;
        }
    }
}
