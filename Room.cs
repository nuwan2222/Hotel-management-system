using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Resources
{
    //admin room management

    //getters and setters
    public class Room
    {
        public int Roomid { get; set; }
        public int Roomno { get; set; }
        public string Roomtype { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public  DateTime CreatedDate { get; set; }
        public  DateTime UpdatedDate  { get; set; }
    }
}
