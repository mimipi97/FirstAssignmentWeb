using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data_Access.Entities;
using System.IO;

namespace Data_Access.Repositories
{
    public class TaskRepository
    {
        private readonly string filePath;

        public TaskRepository(string filePath)
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
                    Task task = new Task();
                    task.Id = Convert.ToInt32(sr.ReadLine());
                    task.Title = sr.ReadLine();
                    task.Description = sr.ReadLine();
                    task.CreatorId = Convert.ToInt32(sr.ReadLine());
                    task.AssigneeId = Convert.ToInt32(sr.ReadLine());
                    task.Grade = Convert.ToInt32(sr.ReadLine());
                    task.CreationDate = Convert.ToDateTime(sr.ReadLine());
                    task.RecentDate = Convert.ToDateTime(sr.ReadLine());
                    task.Status = sr.ReadLine();

                    if (id <= task.Id)
                    {
                        id = task.Id + 1;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return id;
        }

        private void Insert(Task item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                sw.WriteLine(item.Title);
                sw.WriteLine(item.Description);
                sw.WriteLine(item.CreatorId);
                sw.WriteLine(item.AssigneeId);
                sw.WriteLine(item.Grade);
                sw.WriteLine(item.CreationDate);
                sw.WriteLine(item.RecentDate);
                sw.WriteLine(item.Status);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(Task item)
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
                    Entities.Task task = new Task();
                    task.Id = Convert.ToInt32(sr.ReadLine());
                    task.Title = sr.ReadLine();
                    task.Description = sr.ReadLine();
                    task.CreatorId = Convert.ToInt32(sr.ReadLine());
                    task.AssigneeId = Convert.ToInt32(sr.ReadLine());
                    task.Grade = Convert.ToInt32(sr.ReadLine());
                    task.CreationDate = Convert.ToDateTime(sr.ReadLine());
                    task.RecentDate = Convert.ToDateTime(sr.ReadLine());
                    task.Status = sr.ReadLine();

                    if (task.Id != item.Id)
                    {
                        sw.WriteLine(task.Id);
                        sw.WriteLine(task.Title);
                        sw.WriteLine(task.Description);
                        sw.WriteLine(task.CreatorId);
                        sw.WriteLine(task.AssigneeId);
                        sw.WriteLine(task.Grade);
                        sw.WriteLine(task.CreationDate);
                        sw.WriteLine(task.RecentDate);
                        sw.WriteLine(task.Status);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        sw.WriteLine(item.Title);
                        sw.WriteLine(item.Description);
                        sw.WriteLine(item.CreatorId);
                        sw.WriteLine(item.AssigneeId);
                        sw.WriteLine(item.Grade);
                        sw.WriteLine(item.CreationDate);
                        sw.WriteLine(item.RecentDate);
                        sw.WriteLine(item.Status);
                    }
                }
            }
            finally
            {
                sw.Close();
                sr.Close();
                ifs.Close();
                ofs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public Task GetById(int id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Task task = new Task();
                    task.Id = Convert.ToInt32(sr.ReadLine());
                    task.Title = sr.ReadLine();
                    task.Description = sr.ReadLine();
                    task.CreatorId = Convert.ToInt32(sr.ReadLine());
                    task.AssigneeId = Convert.ToInt32(sr.ReadLine());
                    task.Grade = Convert.ToInt32(sr.ReadLine());
                    task.CreationDate = Convert.ToDateTime(sr.ReadLine());
                    task.RecentDate = Convert.ToDateTime(sr.ReadLine());
                    task.Status = sr.ReadLine();

                    if (task.Id == id)
                    {
                        return task;
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return null;
        }

        public List<Task> GetAllYours(int CreatorId, int AssigneeId)
        {
            List<Task> result = new List<Task>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Task task = new Task();
                    task.Id = Convert.ToInt32(sr.ReadLine());
                    task.Title = sr.ReadLine();
                    task.Description = sr.ReadLine();
                    task.CreatorId = Convert.ToInt32(sr.ReadLine());
                    task.AssigneeId = Convert.ToInt32(sr.ReadLine());
                    task.Grade = Convert.ToInt32(sr.ReadLine());
                    task.CreationDate = Convert.ToDateTime(sr.ReadLine());
                    task.RecentDate = Convert.ToDateTime(sr.ReadLine());
                    task.Status = sr.ReadLine();

                    if (task.CreatorId == CreatorId || task.AssigneeId == AssigneeId)
                    {
                        result.Add(task);
                    }
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public void Delete(Task item)
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
                    Task task = new Task();
                    task.Id = Convert.ToInt32(sr.ReadLine());
                    task.Title = sr.ReadLine();
                    task.Description = sr.ReadLine();
                    task.CreatorId = Convert.ToInt32(sr.ReadLine());
                    task.AssigneeId = Convert.ToInt32(sr.ReadLine());
                    task.Grade = Convert.ToInt32(sr.ReadLine());
                    task.CreationDate = Convert.ToDateTime(sr.ReadLine());
                    task.RecentDate = Convert.ToDateTime(sr.ReadLine());
                    task.Status = sr.ReadLine();

                    if (task.Id != item.Id)
                    {
                        sw.WriteLine(task.Id);
                        sw.WriteLine(task.Title);
                        sw.WriteLine(task.Description);
                        sw.WriteLine(task.CreatorId);
                        sw.WriteLine(task.AssigneeId);
                        sw.WriteLine(task.Grade);
                        sw.WriteLine(task.CreationDate);//warning
                        sw.WriteLine(task.RecentDate);
                        sw.WriteLine(task.Status);
                    }
                }
            }
            finally
            {
                sw.Close();
                sr.Close();
                ifs.Close();
                ofs.Close();
            }

            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        public void Save(Task item)
        {
            if (item.Id > 0)
                Update(item);
            else
                Insert(item);
        }
    }
}