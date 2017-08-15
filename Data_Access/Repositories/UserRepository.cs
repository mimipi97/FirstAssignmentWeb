using System;
using System.Collections.Generic;
using Data_Access.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private void Insert(User item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
INSERT INTO Users (FirstName, LastName, Username, [Password] ) 
VALUES (@FirstName, @LastName, @Username, @Password)
";
            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@FirstName";
            parameter.Value = item.FirstName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@LastName";
            parameter.Value = item.LastName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Username";
            parameter.Value = item.Username;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Password";
            parameter.Value = item.Password;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        private void Update(User item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
UPDATE Users 
SET 
    FirstName=@FirstName, 
    LastName=@LastName, 
    Username=@Username,
    [Password]=@Password
WHERE Id=@Id
";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = item.Id;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@FirstName";
            parameter.Value = item.FirstName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@LastName";
            parameter.Value = item.LastName;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Username";
            parameter.Value = item.Username;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Password";
            parameter.Value = item.Password;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public User GetById(int id)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                using (connection)
                {
                    IDbCommand command = connection.CreateCommand();
                    command.CommandText =
@"
SELECT * FROM Users 
WHERE 
Id=@Id 
";

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@Id";
                    parameter.Value = id;
                    command.Parameters.Add(parameter);

                    IDataReader reader = command.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = (int)reader["Id"];
                            user.FirstName = (string)reader["FirstName"];
                            user.LastName = (string)reader["LastName"];
                            user.Username = (string)reader["Username"];
                            user.Password = (string)reader["Password"];

                            return user;
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return null;
        }

        public List<User> GetAll()
        {
            List<User> resultSet = new List<User>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"
SELECT * FROM Users 
";

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new User()
                        {
                            Id = (int)reader["Id"],
                            FirstName = (string)reader["FirstName"],
                            LastName = (string)reader["LastName"],
                            Username = (string)reader["Username"],
                            Password = (string)reader["Password"]
                        });
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return resultSet;
        }

        public void Delete(User item)
        {
            IDbConnection connection = new SqlConnection(connectionString);


            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
DELETE FROM Users 
WHERE Id=@Id
";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = item.Id;
            command.Parameters.Add(parameter);

            try
            {
                connection.Open();

                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public void Save(User item)
        {
            if (item.Id > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            IDbConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                using (connection)
                {
                    IDbCommand command = connection.CreateCommand();
                    command.CommandText =
@"
SELECT * FROM [Users] 
WHERE 
    [Username]=@Username 
    AND [Password]=@Password
";

                    IDataParameter parameter = command.CreateParameter();
                    parameter.ParameterName = "@Username";
                    parameter.Value = username;
                    command.Parameters.Add(parameter);

                    parameter = command.CreateParameter();
                    parameter.ParameterName = "@Password";
                    parameter.Value = password;
                    command.Parameters.Add(parameter);

                    IDataReader reader = command.ExecuteReader();

                    using (reader)
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.Id = (int)reader["Id"];
                            user.FirstName = (string)reader["FirstName"];
                            user.LastName = (string)reader["LastName"];
                            user.Username = (string)reader["Username"];
                            user.Password = (string)reader["Password"];

                            return user;
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
            }

            return null;
        }
    }
}