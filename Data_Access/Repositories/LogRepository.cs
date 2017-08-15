using System;
using System.Collections.Generic;
using Data_Access.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Data_Access.Repositories
{
    public class LogRepository
    {
        private readonly string connectionString;

        public LogRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private void Insert(TimeLog item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
INSERT INTO TimeLogs (ParentTaskId,ParentUserId,TimeSpent,CreationDate) 
VALUES (@ParentTaskId,@ParentUserId,@TimeSpent,@CreationDate) 
";
            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@ParentTaskId";
            parameter.Value = item.ParentTaskId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ParentUserId";
            parameter.Value = item.ParentUserId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@TimeSpent";
            parameter.Value = item.TimeSpent;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@CreationDate";
            parameter.Value = item.CreationDate;
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

        private void Update(TimeLog item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
UPDATE TimeLogs 
SET 
    ParentTaskId=@ParentTaskId, 
    ParentUserId=@ParentUserId, 
    TimeSpent=@TimeSpent,
    CreationDate=@CreationDate
WHERE Id=@Id
";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = item.Id;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ParentTaskId";
            parameter.Value = item.ParentTaskId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@ParentUserId";
            parameter.Value = item.ParentUserId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@TimeSpent";
            parameter.Value = item.TimeSpent;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@CreationDate";
            parameter.Value = item.CreationDate;
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

        public TimeLog GetById(int id)
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
SELECT * FROM TimeLogs 
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
                            TimeLog log = new TimeLog();
                            log.Id = (int)reader["Id"];
                            log.ParentTaskId = (int)reader["ParentTaskId"];
                            log.ParentUserId = (int)reader["ParentUserId"];
                            log.TimeSpent = (int)reader["TimeSpent"];
                            log.CreationDate = (DateTime)reader["CreationDate"];
                            

                            return log;
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

        public List<TimeLog> GetAll(int TaskId)
        {
            List<TimeLog> resultSet = new List<TimeLog>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"
SELECT * FROM TimeLogs 
WHERE 
    [ParentTaskId]=@ParentTaskId 
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@ParentTaskId";
                parameter.Value = TaskId;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new TimeLog()
                        {
                            Id = (int)reader["Id"],
                            ParentTaskId = (int)reader["ParentTaskId"],
                            ParentUserId = (int)reader["ParentuserId"],
                            TimeSpent = (int)reader["TimeSpent"],
                            CreationDate = (DateTime)reader["CreationDate"]
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

        public void Delete(TimeLog item)
        {
            IDbConnection connection = new SqlConnection(connectionString);


            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
DELETE FROM TimeLogs 
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

        public void Save(TimeLog item)
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
    }
}

