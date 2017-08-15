using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositories
{
    public class RepositoryFactory
    {
        public static UserRepository GetUserRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskManagerConnectionString"].ConnectionString;
            return new UserRepository(connectionString);
        }

        public static TaskRepository GetTaskRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskManagerConnectionString"].ConnectionString;
            return new TaskRepository(connectionString);
        }

        public static CommentRepository GetCommentRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskManagerConnectionString"].ConnectionString;
            return new CommentRepository(connectionString);
        }

        public static LogRepository GetLogRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TaskManagerConnectionString"].ConnectionString;
            return new LogRepository(connectionString);
        }
    }
}

