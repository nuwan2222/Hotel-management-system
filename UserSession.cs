using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Properties
{
    //getters and setters
        public static class UserSession
        {
            public static string CurrentUsername { get; set; }
            public static string CurrentUserrole { get; set; }
            public static bool IsLoggedIn => !string.IsNullOrEmpty(CurrentUsername);

        //clear the sessions
            public static void Clearsession() 
            {
                CurrentUsername = null;
                CurrentUserrole = null;
            }
        }
    }

