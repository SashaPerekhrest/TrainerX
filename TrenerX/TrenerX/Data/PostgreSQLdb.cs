using System;
using System.Linq;
using Npgsql;
using System.Data;
using TrenerX.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrenerX.Data
{
    public class PostgreSQLdb
    {
        private string ConnectionString = "Server=193.124.125.15; Port=5432; User Id=sasha; Password=trener123trener; Database=trenex";
        private NpgsqlConnection connection;
        private string sql;
        private NpgsqlCommand command;
        private DataTable table;

        public List<PostItemTrener> treners;

        public PostgreSQLdb()
        {
            table = new DataTable();
            treners = new List<PostItemTrener>();
            connection = new NpgsqlConnection(ConnectionString);
        }

        public async void Select()
        {
            connection.Open();
            sql = @"select * from db_select()";
            command = new NpgsqlCommand(sql, connection);
            var som = await command.ExecuteReaderAsync();
            table.Load(som);
            treners.Clear();
            treners = table.Rows.OfType<DataRow>().Select(m => new PostItemTrener()
            {
                ID = m.Field<int>("_id"),
                FullName = m.Field<string>("_full_name"),
                DirOfTraining = m.Field<string>("_dir_of_training"),
                Requirements = m.Field<string>("_requirements"),
                Education = m.Field<string>("_education"),
                TrainingCount = m.Field<string>("_training_count"),
                Price = m.Field<int>("_price"),
                Contacts = m.Field<string>("_contacts"),
                Login = m.Field<string>("_login"),
                Password = m.Field<string>("_pass_word"),
                Image = m.Field<string>("_image"),
                Category = m.Field<int>("_category"),
            }).ToList();

            connection.Close();

            foreach (var trener in treners)
            {
                Console.WriteLine(trener.Password);
            }
        }

        public void Update(PostItemTrener trener)
        {
            connection.Open();
            sql = @"select * from db_update(:_id, :_full_name, :_dir_of_training, :_requirements, :_education, :_training_count, :_price, :_contacts, :_login, :_pass_word, :_image, :_category)";
            command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("_id", trener.ID);
            command.Parameters.AddWithValue("_full_name", trener.FullName);
            command.Parameters.AddWithValue("_dir_of_training", trener.DirOfTraining);
            command.Parameters.AddWithValue("_requirements", trener.Requirements);
            command.Parameters.AddWithValue("_education", trener.Education);
            command.Parameters.AddWithValue("_training_count", trener.TrainingCount);
            command.Parameters.AddWithValue("_price", trener.Price);
            command.Parameters.AddWithValue("_contacts", trener.Contacts);
            command.Parameters.AddWithValue("_login", trener.Login);
            command.Parameters.AddWithValue("_pass_word", trener.Password);
            command.Parameters.AddWithValue("_image", trener.Image);
            command.Parameters.AddWithValue("_category", trener.Category);
            var result = command.ExecuteScalar().ToString();
            connection.Close();
        }

        public void Insert(PostItemTrener trener)
        {
            try
            {
                connection.Open();
                sql = @"select * from db_insert(:_full_name, :_dir_of_training, :_requirements, :_education, :_training_count, :_price, :_contacts, :_login, :_pass_word, :_image, :_category)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_full_name", trener.FullName);
                command.Parameters.AddWithValue("_dir_of_training", trener.DirOfTraining);
                command.Parameters.AddWithValue("_requirements", trener.Requirements);
                command.Parameters.AddWithValue("_education", trener.Education);
                command.Parameters.AddWithValue("_training_count", trener.TrainingCount);
                command.Parameters.AddWithValue("_price", trener.Price);
                command.Parameters.AddWithValue("_contacts", trener.Contacts);
                command.Parameters.AddWithValue("_login", trener.Login);
                command.Parameters.AddWithValue("_pass_word", trener.Password);
                command.Parameters.AddWithValue("_image", trener.Image);
                command.Parameters.AddWithValue("_category", trener.Category);
                var result = (int)command.ExecuteScalar();
                connection.Close();
                trener.ID = result;
                treners.Add(trener);
            } catch (Exception ex)
            {
                connection.Close();
                Console.WriteLine(ex.Message);
            };
        }

        public async void Delete(PostItemTrener trener)
        {
            try
            {
                treners.Remove(trener);
                connection.Open();
                sql = @"select * from db_delete(:_id)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_id", trener.ID);
                await command.ExecuteScalarAsync();
                connection.Close();
            }
            catch { }
        }

        public PostItemTrener GetTrainer(int id)
        {
            return treners.FirstOrDefault(t => t.ID == id);
        }

        public List<PostItemTrener> GetMyTrainers(User user)
        {
            return treners.Where(t => user.TrenersID.Contains(t.ID)).ToList();
        }

        public List<PostItemTrener> GetTrainersAtCategory(int category)
        {
            return treners.Where(t => t.Category == category).ToList();
        }
    }
}
