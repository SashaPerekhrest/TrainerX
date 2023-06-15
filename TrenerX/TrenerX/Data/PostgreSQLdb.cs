using System;
using System.Linq;
using Npgsql;
using System.Data;
using TrenerX.Models;
using System.Collections.Generic;

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
        public List<User> users;
        public List<Request> requests;
        public List<Feedback> feedbacks;

        public PostgreSQLdb()
        {
            table = new DataTable();
            treners = new List<PostItemTrener>();
            users = new List<User>();
            connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
        }

        /// <summary>
        /// Treners functions
        /// </summary>

        public void TrenersSelect()
        {
            try
            {
                sql = @"select * from db_select()";
                command = new NpgsqlCommand(sql, connection);
                var som = command.ExecuteReader();
                table.Clear();
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

                foreach (var trener in table.Rows.OfType<DataRow>())
                {
                    Console.WriteLine(trener.IsNull("_id").ToString() + "////" + trener.Field<int?>("_id").ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("treners//////////////////////////" + ex.Message);
            };
        }

        public void TrenersUpdate(PostItemTrener trener)
        {
            try 
            {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };

        }

        public void TrenersInsert(PostItemTrener trener)
        {
            try
            {
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
                TrenersSelect();
                Console.WriteLine("//////// " + result);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        public async void TrenersDelete(PostItemTrener trener)
        {
            try
            {
                treners.Remove(trener);
                sql = @"select * from db_delete(:_id) cascade";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_id", trener.ID);
                await command.ExecuteScalarAsync();
            }
            catch { }
        }

        public PostItemTrener GetTrainer(int id)
            => treners.FirstOrDefault(t => t.ID == id);

        public PostItemTrener GetTrainerLogin(string login)
            => treners.FirstOrDefault(t => t.Login == login);

        public List<PostItemTrener> GetTrainersAtCategory(int category)
            => treners.Where(t => t.Category == category).ToList();

        public string TrenersLoginCheck(string login)
        {
            sql = @"select * from db_login(:_login)";
            command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("_login", login);
            var result = command.ExecuteScalar().ToString();
            return result;
        }

        /// <summary>
        /// Users functions
        /// </summary>

        public void UsersSelect()
        {
            try 
            {
                sql = @"select * from u_select()";
                command = new NpgsqlCommand(sql, connection);
                table.Clear();
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

                foreach (var trener in users)
                {
                    Console.WriteLine(trener.Password);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("users//////////////////////////" + ex.Message);
            };
        }

        public void UserInsert(User user)
        {
            try
            {
                sql = @"select * from u_insert(:_full_name, :_training_count, :_contacts , :_login, :_pass_word)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_full_name", user.FullName);
                command.Parameters.AddWithValue("_training_count", user.TrainingCount);
                command.Parameters.AddWithValue("_contacts", user.Contacts);
                command.Parameters.AddWithValue("_login", user.Login);
                command.Parameters.AddWithValue("_pass_word", user.Password);
                var result = (int)command.ExecuteScalar();
                UsersSelect();
                Console.WriteLine("/*/");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        public string UsersLoginCheck(string login)
        {
            sql = @"select * from u_login(:_login)";
            command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("_login", login);
            var result = command.ExecuteScalar().ToString();
            return result;
        }

        public void UsersUpdate(User user)
        {
            try
            {
                sql = @"select * from u_update(:_id, :_full_name, :_training_count, :_contacts, :_login, :_pass_word)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_id", user.Id);
                command.Parameters.AddWithValue("_full_name", user.FullName);
                command.Parameters.AddWithValue("_training_count", user.TrainingCount);
                command.Parameters.AddWithValue("_contacts", user.Contacts);
                command.Parameters.AddWithValue("_login", user.Login);
                command.Parameters.AddWithValue("_pass_word", user.Password);
                var result = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        public User GetUser(string login)
            => users.FirstOrDefault(t => t.Login == login);

        public User GetUser(int id)
            => users.FirstOrDefault(t => t.Id == id);

        /// <summary>
        /// Request functions
        /// </summary>

        public void RequestSelect()
        {
            try
            {
                sql = @"select * from r_select()";
                command = new NpgsqlCommand(sql, connection);
                table.Clear();
                table.Load(command.ExecuteReader());
                requests = table.Rows.OfType<DataRow>().Select(m => new Request()
                {
                    ID = m.Field<int>("r_id"),
                    TrenerID = m.Field<int>("t_id_f"),
                    UserID = m.Field<int>("u_id_f"),
                    Confirmation = m.Field<int>("confirmation"),
                }).ToList();

                foreach (var trener in requests)
                {
                    Console.WriteLine("request" + trener.ID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("users//////////////////////////" + ex.Message);
            };
        }

        public void RequestUpdate(Request request)
        {
            try
            {
                sql = @"select * from r_update(:_id, :_confirmation)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_id", request.ID);
                command.Parameters.AddWithValue("_confirmation", request.Confirmation);
                var result = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        public void RequestInsert(int t_id, int u_id, int con)
        {
            try
            {
                sql = @"select * from r_insert(:_t_id_f, :_u_id_f, :_confirmation)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_t_id_f",t_id);
                command.Parameters.AddWithValue("_u_id_f", u_id);
                command.Parameters.AddWithValue("_confirmation", con);
                var result = (int)command.ExecuteScalar();
                RequestSelect();
                Console.WriteLine("/*/");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        public async void RequestDelete(Request request)
        {
            try
            {
                requests.Remove(request);
                sql = @"select * from r_delete(:_id)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_id", request.ID);
                await command.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        public List<PostItemTrener> GetMyTrainersR(User user)
        {
            var myTrenersID = requests.Where(x => x.UserID == user.Id && x.Confirmation == 1)
                                      .Select(x => x.TrenerID)
                                      .ToList();
            var result = treners.Where(t => myTrenersID.Contains(t.ID)).ToList();
            return result;
        }

        public List<User> GetMyUsersR(PostItemTrener trener)
        {
            var myUsersID = requests.Where(x => x.TrenerID == trener.ID && x.Confirmation == 0)
                                    .Select(x => x.UserID)
                                    .ToList();
            var result = users.Where(t => myUsersID.Contains(t.Id)).ToList();
            return result;
        }

        public Request GetRequest(int t_id, int u_id)
            => requests.Where(x => x.TrenerID == t_id)
                       .Where(x => x.UserID == u_id)
                       .FirstOrDefault();


        /// <summary>
        /// Request functions
        /// </summary>

        public void FeedbackSelect()
        {
            table.Clear();
            try
            {
                sql = @"select * from f_select()";
                command = new NpgsqlCommand(sql, connection);
                table.Clear();
                table.Load(command.ExecuteReader());
                feedbacks = table.Rows.OfType<DataRow>().Select(m => new Feedback()
                {
                    ID = m.Field<int>("id"),
                    TrenerID = m.Field<int>("t_id_f"),
                    UserID = m.Field<int>("u_id_f"),
                    Review = m.Field<string>("review"),
                }).ToList();

                foreach (var trener in feedbacks)
                {
                    Console.WriteLine("feedback" + trener.UserName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("users//////////////////////////" + ex.Message);
            };
        }

        public void FeedbackUpdate(Feedback feedback)
        {
            try
            {
                sql = @"select * from f_update(:_id, :_review)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_id", feedback.ID);
                command.Parameters.AddWithValue("_review", feedback.Review);
                var result = command.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        public void FeedbackInsert(int t_id, int u_id, string con)
        {
            try
            {
                sql = @"select * from f_insert(:_t_id_f, :_u_id_f, :_review)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_t_id_f", t_id);
                command.Parameters.AddWithValue("_u_id_f", u_id);
                command.Parameters.AddWithValue("_review", con);
                var result = (int)command.ExecuteScalar();
                FeedbackSelect();
                Console.WriteLine("/*/");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        public async void FeedbackDelete(Feedback feedback)
        {
            try
            {
                feedbacks.Remove(feedback);
                sql = @"select * from f_delete(:_id)";
                command = new NpgsqlCommand(sql, connection);
                command.Parameters.AddWithValue("_id", feedback.ID);
                await command.ExecuteScalarAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }

        public List<Feedback> GetTrenersFeedbacks(PostItemTrener trener)
            => feedbacks.Where(feedback => feedback.TrenerID == trener.ID).ToList();
    }
}
