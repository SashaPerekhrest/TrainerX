using System;
using System.Linq;
using Npgsql;
using System.Data;
using TrenerX.Models;
using System.Collections.Generic;

namespace TrenerX.Data
{
    public class PostUsersDB
    {
        private string ConnectionString = "Server=193.124.125.15; Port=5432; User Id=sasha; Password=trener123trener; Database=trenex";
        private NpgsqlConnection connection;
        private string sql;
        private NpgsqlCommand command;
        private DataTable table;

        public List<User> users;

        public PostUsersDB()
        {
            table = new DataTable();
            users = new List<User>();
            connection = new NpgsqlConnection(ConnectionString);
        }

        public void Select()
        {
            connection.Open();
            sql = @"select * from u_select()";
            command = new NpgsqlCommand(sql, connection);
            table.Load(command.ExecuteReader());
            users = table.Rows.OfType<DataRow>().Select(m => new User()
            {
                Id = m.Field<int>("u_id"),
                FullName = m.Field<string>("full_name"),
                TrainingCount = m.Field<string>("training_count"),
                Contacts = m.Field<string>("contacts"),
                Login = m.Field<string>("login"),
                Password = m.Field<string>("pass_word"),
            }).ToList();

            connection.Close();

            foreach (var trener in users)
            {
                Console.WriteLine(trener.Password);
            }
        }

        public string LoginCheck(string login)
        {
            connection.Open();
            sql = @"select * from u_login(:_login)";
            command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("_login", login);
            var result = command.ExecuteScalar().ToString();
            connection.Close();
            return result;
        }

        public void Update(User trener)
        {
            try
            {
                connection.Open();
                sql = @"select * from u_update(:_id, :_full_name, :_training_count, :_contacts, :_login, :_pass_word)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_id", trener.Id);
                command.Parameters.AddWithValue("_full_name", trener.FullName);
                command.Parameters.AddWithValue("_training_count", trener.TrainingCount);
                command.Parameters.AddWithValue("_contacts", trener.Contacts);
                command.Parameters.AddWithValue("_login", trener.Login);
                command.Parameters.AddWithValue("_pass_word", trener.Password);
                var result = command.ExecuteScalar().ToString();
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                Console.WriteLine(ex.Message);
            };
        }

        public User GetUser(string login)
        {
            return users.FirstOrDefault(t => t.Login == login);
        }
    }
}
