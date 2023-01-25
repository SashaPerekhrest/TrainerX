using System;
using System.Collections.Generic;
using System.Text;

namespace TrenerX.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string TrainingCount { get; set; }
        public string Contacts { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public List<int> TrenersID { get; set; }

        public void SetTrenersID()
        {
            try
            {
                TrenersID = new List<int>();
                var col = TrainingCount.Split(' ');
                if (col.Length > 0)
                    foreach (var t in col)
                    {
                        if (t.Length != 0 && t != null)
                            TrenersID.Add(int.Parse(t));
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
