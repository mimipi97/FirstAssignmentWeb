using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entities;
using System.IO;

namespace Data_Access.Repositories
{
    public class LogRepository
    {
        private readonly string filePath;

        public LogRepository(string filePath)
        {
            this.filePath = filePath;
        }

        private int GetNextId()
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            int id = 1;
            try
            {
                while (!sr.EndOfStream)
                {
                    TimeLog log = new TimeLog();
                    log.Id = Convert.ToInt32(sr.ReadLine());
                    log.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    log.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    log.TimeSpent = Convert.ToInt32(sr.ReadLine());
                    log.CreationDate = Convert.ToDateTime(sr.ReadLine());

                    if (id <= log.Id)
                        id = log.Id + 1;
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return id;
        }

        private void Insert(TimeLog item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                sw.WriteLine(item.ParentTaskId);
                sw.WriteLine(item.ParentUserId);
                sw.WriteLine(item.TimeSpent);
                sw.WriteLine(item.CreationDate);

            }

            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(TimeLog item)
        {
            string fileName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
            string fileFolder = filePath.Substring(0, filePath.LastIndexOf(@"\"));

            string tempFilePath = fileFolder + "temp." + fileName;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    TimeLog log = new TimeLog();
                    log.Id = Convert.ToInt32(sr.ReadLine());
                    log.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    log.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    log.TimeSpent = Convert.ToInt32(sr.ReadLine());
                    log.CreationDate = Convert.ToDateTime(sr.ReadLine());

                    if (log.Id != item.Id)
                    {
                        sw.WriteLine(log.Id);
                        sw.WriteLine(log.ParentTaskId);
                        sw.WriteLine(log.ParentUserId);
                        sw.WriteLine(log.TimeSpent);
                        sw.WriteLine(log.CreationDate);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        sw.WriteLine(item.ParentTaskId);
                        sw.WriteLine(item.ParentUserId);
                        sw.WriteLine(item.TimeSpent);
                        sw.WriteLine(item.CreationDate);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public TimeLog GetById(int id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    TimeLog log = new TimeLog();
                    log.Id = Convert.ToInt32(sr.ReadLine());
                    log.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    log.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    log.TimeSpent = Convert.ToInt32(sr.ReadLine());
                    log.CreationDate = Convert.ToDateTime(sr.ReadLine());

                    if (log.Id == id)
                        return log;
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return null;
        }

        public List<TimeLog> GetAll(int TaskId)
        {
            List<TimeLog> result = new List<TimeLog>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    TimeLog log = new TimeLog();
                    log.Id = Convert.ToInt32(sr.ReadLine());
                    log.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    log.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    log.TimeSpent = Convert.ToInt32(sr.ReadLine());
                    log.CreationDate = Convert.ToDateTime(sr.ReadLine());

                    if (log.ParentTaskId == TaskId)
                        result.Add(log);
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public void Delete(TimeLog item)
        {
            string fileName = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
            string fileFolder = filePath.Substring(0, filePath.LastIndexOf(@"\"));

            string tempFilePath = fileFolder + "temp." + fileName;

            FileStream ifs = new FileStream(filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(ifs);

            FileStream ofs = new FileStream(tempFilePath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(ofs);

            try
            {
                while (!sr.EndOfStream)
                {
                    TimeLog log = new TimeLog();
                    log.Id = Convert.ToInt32(sr.ReadLine());
                    log.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    log.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    log.TimeSpent = Convert.ToInt32(sr.ReadLine());
                    log.CreationDate = Convert.ToDateTime(sr.ReadLine());

                    if (log.Id != log.Id)
                    {
                        sw.WriteLine(log.Id);
                        sw.WriteLine(log.ParentTaskId);
                        sw.WriteLine(log.ParentUserId);
                        sw.WriteLine(log.TimeSpent);
                        sw.WriteLine(log.CreationDate);
                    }
                }
            }
            finally
            {
                sw.Close();
                ofs.Close();
                sr.Close();
                ifs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public void Save(TimeLog item)
        {
            if (item.Id > 0)
                Update(item);
            else
                Insert(item);
        }
    }
}
