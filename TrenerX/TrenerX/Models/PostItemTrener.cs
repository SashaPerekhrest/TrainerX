using System;
using System.Collections.Generic;
using System.Text;

namespace TrenerX.Models
{
    public class PostItemTrener
    {
        public int ID { get; set; }
        public string FullName { get; set; }    // фио
        public string DirOfTraining { get; set; }  // направление тренировок
        public string Requirements { get; set; }   // требования к подготовке
        public string Education { get; set; }   // образование, описание
        public string TrainingCount { get; set; }  //  количество тренировок в неделю (дни недели, 13 - пн ср)
        public int Price { get; set; } // цена тренировки
        public string Contacts { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Image { get; set; } // фото
        public int Category { get; set; }
    }
}
