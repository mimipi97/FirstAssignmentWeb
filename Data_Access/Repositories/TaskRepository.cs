using System;
using System.Collections.Generic;
using Data_Access.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Data_Access.Repositories
{
    public class TaskRepository
    {
        private readonly string connectionString;

        public TaskRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private void Insert(Task item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
INSERT INTO Tasks (Title, [Description], CreatorId, AssigneeId, Grade, CreationDate, RecentDate,Status) 
VALUES (@Title, @Description, @CreatorId, @AssigneeId, @Grade, @CreationDate, @RecentDate, @Status) 
";
            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Title";
            parameter.Value = item.Title;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Description";
            parameter.Value = item.Description;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@CreatorId";
            parameter.Value = item.CreatorId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@AssigneeId";
            parameter.Value = item.AssigneeId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Grade";
            parameter.Value = item.Grade;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@CreationDate";
            parameter.Value = item.CreationDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@RecentDate";
            parameter.Value = item.RecentDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Status";
            parameter.Value = item.Status;
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

        private void Update(Task item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
UPDATE Tasks 
SET 
    Title=@Title, 
    [Description]=@Description, 
    CreatorId=@CreatorId,
    AssigneeId=@AssigneeId,
    Grade=@Grade,
    CreationDate=@CreationDate,
    RecentDate=@RecentDate,
    Status=@Status
WHERE Id=@Id
";

            IDataParameter parameter = command.CreateParameter();
            parameter.ParameterName = "@Id";
            parameter.Value = item.Id;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Title";
            parameter.Value = item.Title;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Description";
            parameter.Value = item.Description;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@CreatorId";
            parameter.Value = item.CreatorId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@AssigneeId";
            parameter.Value = item.AssigneeId;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Grade";
            parameter.Value = item.Grade;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@CreationDate";
            parameter.Value = item.CreationDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@RecentDate";
            parameter.Value = item.RecentDate;
            command.Parameters.Add(parameter);

            parameter = command.CreateParameter();
            parameter.ParameterName = "@Status";
            parameter.Value = item.Status;
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

        public Task GetById(int id)
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
SELECT * FROM Tasks 
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
                            Task task = new Task();
                            task.Id = (int)reader["Id"];
                            task.Title = (string)reader["Title"];
                            task.Description = (string)reader["Description"];
                            task.CreatorId = (int)reader["CreatorId"];
                            task.AssigneeId = (int)reader["AssigneeId"];
                            task.Grade = (int)reader["Grade"];
                            task.CreationDate = (DateTime)reader["CreationDate"];
                            task.RecentDate = (DateTime)reader["RecentDate"];
                            task.Status = (string)reader["Title"];

                            return task;
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

        public List<Task> GetAllYours(int CreatorId, int AssigneeId)
        {
            List<Task> resultSet = new List<Task>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"
SELECT * FROM Tasks 
WHERE 
    [CreatorId]=@CreatorId 
    AND [AssigneeId]=@AssigneeId
";
                IDataParameter parameter = command.CreateParameter();
                parameter.ParameterName = "@CreatorId";
                parameter.Value = CreatorId;
                command.Parameters.Add(parameter);

                parameter = command.CreateParameter();
                parameter.ParameterName = "@AssigneeId";
                parameter.Value = AssigneeId;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                using (reader)
                {
                    while (reader.Read())
                    {
                        resultSet.Add(new Task()
                        {
                            Id = (int)reader["Id"],
                            Title = (string)reader["Title"],
                            Description = (string)reader["Description"],
                            CreatorId = (int)reader["CreatorId"],
                            AssigneeId = (int)reader["AssigneeId"],
                            Grade = (int)reader["Grade"],
                            CreationDate = (DateTime)reader["CreationDate"],
                            RecentDate = (DateTime)reader["RecentDate"],
                            Status = (string)reader["Status"]
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

        public void Delete(Task item)
        {
            IDbConnection connection = new SqlConnection(connectionString);


            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
DELETE FROM Tasks 
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

        public void Save(Task item)
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

        