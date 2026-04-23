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

namespace Hotel.Properties
{
    public partial class Req_and_Feedbacks : Form
    {
        //create object from RequestManeger
        RequestManeger Reqmaneger = new RequestManeger();

        public Req_and_Feedbacks()
        {
            InitializeComponent();
            LoadreqIntoGrid();
        }

        private void LoadreqIntoGrid()
        {

            //session handle
             if (!UserSession.IsLoggedIn)
              {
                  MessageBox.Show("Please login to see bookings.", "Login Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  return;
              }

             //requests list
            List<Requests> req = Reqmaneger.GetAllRequests();
            dataGridView1.DataSource = req;

            //feddbacks list
            List<Feedbacks> feedback = Reqmaneger.GetAllFeedbacks();
            dataGridView2.DataSource = feedback;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
