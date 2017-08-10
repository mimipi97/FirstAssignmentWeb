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
            string path = ConfigurationManager.AppSettings["dataPath"];
            return new UserRepository(path + @"\users.txt");
        }

        public static TaskRepository GetTaskRepository()
        {
            string path = ConfigurationManager.AppSettings["dataPath"];
            return new TaskRepository(path + @"\tasks.txt");
        }

        public static CommentRepository GetCommentRepository()
        {
            string path = ConfigurationManager.AppSettings["dataPath"];
            return new CommentRepository(path + @"\comments.txt");
        }

        public static LogRepository GetLogRepository()
        {
            string path = ConfigurationManager.AppSettings["dataPath"];
            return new LogRepository(path + @"\logs.txt");
        }
    }
}

