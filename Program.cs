using Hotel.Properties;
using Hotel.Resources;

namespace Hotel
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new signup());
            //Application.Run(new Form3());
           Application.Run(new Login());
           // Application.Run(new Guest_s_dashboard());
            // Application.Run(new Edit_profile());
            //Application.Run(new Admin_dashboard());
            // Application.Run(new guests_room());
            //Application.Run(new guests_requests());
            //Application.Run(new gallry());
        }
    }
}