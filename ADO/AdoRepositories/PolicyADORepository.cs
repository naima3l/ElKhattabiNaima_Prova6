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
    public class PolicyADORepository : IPolicyRepository
    {
        const string connectionString = @"Data Source = (localdb)\mssqllocaldb;" +
                                    "Initial Catalog = Assicurazione;" +
                                    "Integrated Security = true;";
        public bool Add(Policy item)
        {
            throw new NotImplementedException();
        }

        public bool AddPolicyToExistingUser(Policy policy)
        {
            bool check = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "insert into Policies values (@number,@date,@payment,@type)";
                command.Parameters.AddWithValue("@number", policy.PolicyNumber);
                command.Parameters.AddWithValue("@date", policy.ExpirationDate);
                command.Parameters.AddWithValue("@payment", policy.MontlyPayment);
                command.Parameters.AddWithValue("@type", (EnumPolicyType)policy.Type);

                command.ExecuteNonQuery();
                check = true;
            }
            return check;
        }

        public bool CheckPolicyNumber(int num)
        {
            int i = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.CommandText = "select * from Policies where PolicyNumber = @num";
                command.Parameters.AddWithValue("@num", num);

                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {
                    var n = (string)reader["PolicyNumber"];
                    i++;
                }
            }
            if (i == 0)
            {
                return false;
            }
            else return true;
        }

        public bool Delete(Policy item)
        {
            throw new NotImplementedException();
        }

        public List<Policy> Fetch()
        {
            List<Policy> policies = new List<Policy>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.CommandText = "select * from Policies";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var num = (int)reader["PolicyNumber"];
                    var payment = (decimal)reader["MontlyPayment"];
                    var date = (DateTime)reader["ExpirationDate"];
                    var type = (EnumPolicyType)reader["Type"];
                    var id = (int)reader["Id"];


                    policies.Add(new Policy
                    {
                        Id = id,
                        PolicyNumber = num,
                        MontlyPayment = payment,
                        ExpirationDate = date,
                        Type = type
                    });
                }

            }
            return policies;
        }

        public List<Policy> FetchUserPolicies(User user)
        {
            List<Policy> policies = new List<Policy>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.CommandText = "select * from Policies where Policies.UserId = @id";
                command.Parameters.AddWithValue("@id", user.Id);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var num = (int)reader["PolicyNumber"];
                    var payment = (decimal)reader["MontlyPayment"];
                    var date = (DateTime)reader["ExpirationDate"];
                    var type = (EnumPolicyType)reader["Type"];
                    var id = (int)reader["Id"];


                    policies.Add(new Policy
                    {
                        Id = id,
                        PolicyNumber = num,
                        MontlyPayment = payment,
                        ExpirationDate = date,
                        Type = type
                    });
                }

            }
            return policies;
        }

        public Policy GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Policy GetPolicyByNumber(int num)
        {
            Policy policy = new Policy();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.CommandText = "select * from Policies where PolicyNumber = @num";
                command.Parameters.AddWithValue("@num", num);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var n = (int)reader["PolicyNumber"];
                    var payment = (decimal)reader["MontlyPayment"];
                    var date = (DateTime)reader["ExpirationDate"];
                    var type = (EnumPolicyType)reader["Type"];
                    var id = (int)reader["Id"];

                    policy.Id = id;
                    policy.MontlyPayment = payment;
                    policy.PolicyNumber = num;
                    policy.ExpirationDate = date;
                    policy.Type = type;
                }
            }
            return policy;
        }

        public bool PosticipateExpirationDate_Disconnected(Policy policy)
        {
            bool check = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = "update Policies set PolicyNumber = @num, ExpirationDate = @date, ExpirationDate = @date, MontlyPayment = @payment, Type = @type where Id = @id";
                command.Parameters.AddWithValue("@num", policy.PolicyNumber);
                command.Parameters.AddWithValue("@date", policy.ExpirationDate);
                command.Parameters.AddWithValue("@payment", policy.MontlyPayment);
                command.Parameters.AddWithValue("@type", (int)policy.Type);
                command.Parameters.AddWithValue("@id", policy.Id);

                command.ExecuteNonQuery();
                check = true;
            }

            return check;
        }

        public bool Update(Policy item)
        {
            throw new NotImplementedException();
        }
    }
}
