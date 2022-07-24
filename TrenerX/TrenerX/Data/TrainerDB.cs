using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TrenerX.Models;

namespace TrenerX.Data
{
    public class TrainerDB
    {
        readonly SQLiteAsyncConnection db;

        public TrainerDB(string connectionS)
        {
            db = new SQLiteAsyncConnection(connectionS);

            db.CreateTableAsync<ItemTrainer>().Wait();
        }

        public Task<List<ItemTrainer>> GetTrainersAsync() => 
            db.Table<ItemTrainer>().ToListAsync();

        public Task<ItemTrainer> GetTrainerAsync(int id) => 
            db.Table<ItemTrainer>().Where(i=> i.ID == id).FirstOrDefaultAsync();

        public Task<int> SaveTrainerAsync(ItemTrainer trainer)
        {
            if (trainer.ID != 0)
                return db.UpdateAsync(trainer);
            else
                return db.InsertAsync(trainer);
        }

        public Task<int> DeleteTrainerAsync(ItemTrainer trainer) =>
            db.DeleteAsync(trainer);
    }
}
