using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Properties
{
    //getters and setters
    public class Requests
    {
        public string GuestName { get; set; }
        public int RoomId{ get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
       
    }

    public class Feedbacks
    {
        public int FeedbackId { get; set; }
        public string Username { get; set; }
        public string Comments { get; set; }
        public int Star_ratings { get; set; }
        public DateTime FeedbackDate { get; set; }
        
    }
}
