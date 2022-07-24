using SQLite;

namespace TrenerX.Models
{
    public class ItemTrainer
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; } 
        public int PrevID { get; set; }

        public string FullName { get; set; }    // фио
        public string DirOfTraining { get; set; }  // направление тренировок
        public string Requirements { get; set; }   // требования к подготовке
        public string Education { get; set; }   // образование, описание
        public string TrainingCount { get; set; }  //  количество тренировок в неделю (дни недели, 13 - пн ср)
        public string Image { get; set; } // фото
        public bool IsMine { get; set; } // мой ли тренер
    }
}
