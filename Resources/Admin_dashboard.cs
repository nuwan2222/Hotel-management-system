using System.Data;
using Hotel.Properties;
using Microsoft.Data.SqlClient;

namespace Hotel.Resources
{
    public partial class Admin_dashboard : Form
    {
        string connectionString = "Data Source=desktop-g0sanc0\\sqlexpress;Initial Catalog=Hotel;Integrated Security=True;TrustServerCertificate=True;";
        private string currentUsername;

        public Admin_dashboard()
        {
            InitializeComponent();

            //main.jpg
             panel1.Visible = true;
             panel1.Size = new Size(800, 400);
            panel1.Location = new Point(350, 150);

            view_users.Visible = true;

            // Session handling
            if (UserSession.IsLoggedIn)
            {

                currentUsername = UserSession.CurrentUsername;

                if (string.IsNullOrEmpty(currentUsername))
                {
                    MessageBox.Show("Session username is empty. Please login again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                textBox1.Text = UserSession.CurrentUsername;
                textBox1.ReadOnly = true;
            }
            else
            {
                textBox1.Text = "Guest";
            }

            view_booking.Visible = false;
            change_pw.Visible = false;
            view_users.Visible = false;

        }


        //button function
        private void PlaceControlBottomCenter(Control ctrl)
        {
            if (ctrl.Parent != null)
            {
                ctrl.Location = new Point(
                    (ctrl.Parent.Width - ctrl.Width) / 2,
                    ctrl.Parent.Height - ctrl.Height - 20
                );
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void button10_Click(object sender, EventArgs e)
        {

        }



        //logout button
        private void button15_Click(object sender, EventArgs e)
        {
            //clear the session
            UserSession.Clearsession();
            Login login = new Login();
            login.Show();
            this.Hide();



        }

        private void button16_Click(object sender, EventArgs e) // ROOM
        {


            Admin_room_management admin_Room_Management = new Admin_room_management();
            admin_Room_Management.Show();


        }

        private void panelRoomManagement_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e) // USER
        {
            //hide panels
            panel1.Visible = false;
            view_booking.Visible = false;
            change_pw.Visible = false;

            //show panels
            view_users.Visible = true;

            //new size
            view_users.Size = new Size(350, 200);
            view_users.Location = new Point(400, 150);

            //get button to the center
            PlaceControlBottomCenter(button5);

        }

        private void button18_Click(object sender, EventArgs e) //BOOK
        {
            //hide panels
            panel1.Visible = false;
            change_pw.Visible = false;
            view_users.Visible = false;


            //show panels
            view_booking.Visible = true;

            //new sizes
            view_booking.Size = new Size(200, 200);
            view_booking.Location = new Point(400, 150);



            //get button to the center
            PlaceControlBottomCenter(button8);

        }

        private void button19_Click(object sender, EventArgs e) //CHECK
        {
            Req_and_Feedbacks req_And_Feedbacks = new Req_and_Feedbacks();
            req_And_Feedbacks.Show();

            //hide panels
            panel1.Visible = false;
            change_pw.Visible = false;
            view_users.Visible = false;
            view_booking.Visible = false;

        }

        private void button21_Click(object sender, EventArgs e) //SETTINGS
        {
            //hide panels
            panel1.Visible = false;
            view_users.Visible = false;
            view_booking.Visible = false;


            //show panels
            change_pw.Visible = true;

            //new sizes
            change_pw.Size = new Size(350, 200);
            change_pw.Location = new Point(400, 150);


            //get button to the center
            PlaceControlBottomCenter(button14);

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void search_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            //show the reports part
            Reports form = new Reports();
            form.Show();

        }

        private void addroom_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Admin_user_managementcs admin_User_Managementcs = new Admin_user_managementcs();
            admin_User_Managementcs.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Admin_view_Booking admin_View_Booking = new Admin_view_Booking();
            admin_View_Booking.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Admin_change_password admin_Change_Password = new Admin_change_password();
            admin_Change_Password.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }



}
