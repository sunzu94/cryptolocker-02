using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace PasswordService.Models
{
    public static class Dal
    {
        private static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        
        public static Password[] GetPasswords()
        {
            IList<Password> list = new List<Password>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("select * from Passwords", connection))
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Password
                        {
                            Id = reader.GetString(reader.GetOrdinal("Id")),
                            PublicKey = reader.GetString(reader.GetOrdinal("PublicKey")),
                            PrivateKey = reader.GetString(reader.GetOrdinal("PrivateKey")),
                            TimeStamp = reader.GetDateTime(reader.GetOrdinal("TimeStamp")),
                            UserInfo = reader.GetString(reader.GetOrdinal("UserInfo"))
                        });
                    }
                }
            }

            return list.ToArray();
        }

        public static Password GetPassword(string id)
        {
            Password password = null;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("select * from Passwords where id = @id", connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Password
                        {
                            Id = reader.GetString(reader.GetOrdinal("Id")),
                            PublicKey = reader.GetString(reader.GetOrdinal("PublicKey")),
                            PrivateKey = reader.GetString(reader.GetOrdinal("PrivateKey")),
                            TimeStamp = reader.GetDateTime(reader.GetOrdinal("TimeStamp")),
                            UserInfo = reader.GetString(reader.GetOrdinal("UserInfo"))
                        };
                    }
                }
            }

            return null;
        }

        public static void InsertPassword(Password password)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = new SqlCommand("insert into Passwords(Id,PublicKey,PrivateKey,UserInfo,Timestamp) values (@Id,@PublicKey,@PrivateKey,@UserInfo,@Timestamp)", connection))
            {
                command.Parameters.AddWithValue("@Id", password.Id);
                command.Parameters.AddWithValue("@PublicKey", password.PublicKey);
                command.Parameters.AddWithValue("@PrivateKey", password.PrivateKey);
                command.Parameters.AddWithValue("@UserInfo", password.UserInfo);
                command.Parameters.AddWithValue("@Timestamp", password.TimeStamp);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}