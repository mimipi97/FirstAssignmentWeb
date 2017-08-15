using System;
using System.Collections.Generic;
using Data_Access.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Data_Access.Repositories
{
    public class CommentRepository
    {
        private readonly string connectionString;

        public CommentRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private void Insert(Comment item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
INSERT INTO Comments (ParentTaskId,ParentUserId,CommentBody) 
VALUES (@ParentTaskId,@ParentUserId,@CommentBody) 
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
            parameter.ParameterName = "@CommentBody";
            parameter.Value = item.CommentBody;
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

        private void Update(Comment item)
        {
            IDbConnection connection = new SqlConnection(connectionString);

            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
UPDATE Comments 
SET 
    ParentTaskId=@ParentTaskId, 
    ParentUserId=@ParentUserId,
    CommentBody=@CommentBody
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
            parameter.ParameterName = "@CommentBody";
            parameter.Value = item.CommentBody;
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

        public Comment GetById(int id)
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
SELECT * FROM Comments 
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
                            Comment comment = new Comment();
                            comment.Id = (int)reader["Id"];
                            comment.ParentTaskId = (int)reader["ParentTaskId"];
                            comment.ParentUserId = (int)reader["ParentUserId"];
                            comment.CommentBody = (string)reader["CommentBody"];


                            return comment;
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

        public List<Comment> GetAll(int TaskId)
        {
            List<Comment> resultSet = new List<Comment>();
            IDbConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText =
@"
SELECT * FROM Comments 
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
                        resultSet.Add(new Comment()
                        {
                            Id = (int)reader["Id"],
                            ParentTaskId = (int)reader["ParentTaskId"],
                            ParentUserId = (int)reader["ParentuserId"],
                            CommentBody = (string)reader["CommentBody"]
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

        public void Delete(Comment item)
        {
            IDbConnection connection = new SqlConnection(connectionString);


            IDbCommand command = connection.CreateCommand();
            command.CommandText =
@"
DELETE FROM Comments
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

        public void Save(Comment item)
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