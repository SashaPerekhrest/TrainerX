using System;
using System.Collections.Generic;
using System.Text;

namespace TrenerX.Models
{
    public class Request
    {
        public int ID { get; set; }
        public int TrenerID { get; set; }
        public int UserID { get; set; }
        public int Confirmation { get; set; }
    }
}
