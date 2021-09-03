using ElKhattabiNaima_Prova6.Core.Interfaces;
using ElKhattabiNaima_Prova6.Core.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElKhattabiNaima_Prova6.ADO.AdoRepositories
{
    public class UserADORepository : IUserRepository
    {
        const string connectionString = @"Data Source = (localdb)\mssqllocaldb;" +
                                    "Initial Catalog = Assicurazione;" +
                                    "Integrated Security = true;";
        public bool Add(User item)
        {
            bool check = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "insert into Users values (@cf,@name,@lastname)";
                command.Parameters.AddWithValue("@cf", item.CF);
                command.Parameters.AddWithValue("@name", item.Name);
                command.Parameters.AddWithValue("@lastname", item.LastName);

                command.ExecuteNonQuery();
                check = true;
            }
            return check;
        }

        public bool Delete(User item)
        {
            throw new NotImplementedException();
        }

        public List<User> Fetch()
        {
            throw new NotImplementedException();
        }

        public List<User> FetchUsersByPolicy()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.CommandText = "select * from Users join Policies on users.Id = Policies.UserId where Policies.Type = '2'";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var cf = (string)reader["CF"];
                    var name = (string)reader["Name"];
                    var lastname = (string)reader["LastName"];

                    users.Add(new User
                    {
                        CF = cf,
                        Name = name,
                        LastName = lastname
                    });
                }
            }
            return users;
        }

        public bool GetByCF(string cf)
        {
            int i = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.CommandText = "select * from Users where CF = @cf";
                command.Parameters.AddWithValue("@cf", cf);

                SqlDataReader reader = command.ExecuteReader();

               
                while (reader.Read())
                {
                    var codf = (string)reader["CF"];
                    var name = (string)reader["Name"];
                    var lastname = (string)reader["LastName"];
                    i++;
                }
            }
            if (i == 0)
            {
                return false;
            }
            else return true;
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByCF(string cf)
        {
            User user = new User();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.CommandText = "select * from Users where CF = @cf";
                command.Parameters.AddWithValue("@cf", cf);

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    var codf = (string)reader["CF"];
                    var name = (string)reader["Name"];
                    var lastname = (string)reader["LastName"];

                    user.CF = codf;
                    user.Name = name;
                    user.LastName = lastname;
                }
            }
            
            return user;
        }

        public bool Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
