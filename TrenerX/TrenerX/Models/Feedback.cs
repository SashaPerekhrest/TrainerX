using System;
using System.Collections.Generic;
using System.Text;

namespace TrenerX.Models
{
    public class Feedback
    {
        public int ID { get; set; }
        public int TrenerID { get; set; }
        public int UserID { get; set; }
        public string Review { get; set; }
        public string UserName
        {
            get
            {
                return App.dataBase.GetUser(UserID).FullName;
            }
        }

    }
}
