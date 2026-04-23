using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Resources
{
    //getters and setters
    public class ViewBooking
    {
        public int BookingId { get; set; }
        public int RoomNo { get; set; }
        public string GuestName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime BookingDate { get; set; }

    }
}
