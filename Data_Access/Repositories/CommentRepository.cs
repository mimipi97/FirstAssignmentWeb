using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entities;
using System.IO;

namespace Data_Access.Repositories
{
    public class CommentRepository
    {
        private readonly string filePath;

        public CommentRepository(string filePath)
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
                    Comment comment = new Comment();
                    comment.Id = Convert.ToInt32(sr.ReadLine());
                    comment.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    comment.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    comment.CommentBody = sr.ReadLine();

                    if (id <= comment.Id)
                        id = comment.Id + 1;
                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return id;
        }

        private void Insert(Comment item)
        {
            item.Id = GetNextId();

            FileStream fs = new FileStream(filePath, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            try
            {
                sw.WriteLine(item.Id);
                sw.WriteLine(item.ParentTaskId);
                sw.WriteLine(item.ParentUserId);
                sw.WriteLine(item.CommentBody);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        private void Update(Comment item)
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
                    Comment comment = new Comment();
                    comment.Id = Convert.ToInt32(sr.ReadLine());
                    comment.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    comment.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    comment.CommentBody = sr.ReadLine();

                    if (comment.Id != item.Id)
                    {
                        sw.WriteLine(comment.Id);
                        sw.WriteLine(comment.ParentTaskId);
                        sw.WriteLine(comment.ParentUserId);
                        sw.WriteLine(comment.CommentBody);
                    }
                    else
                    {
                        sw.WriteLine(item.Id);
                        sw.WriteLine(item.ParentTaskId);
                        sw.WriteLine(item.ParentUserId);
                        sw.WriteLine(item.CommentBody);
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

        public Comment GetById(int id)
        {
            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Comment comment = new Comment();
                    comment.Id = Convert.ToInt32(sr.ReadLine());
                    comment.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    comment.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    comment.CommentBody = sr.ReadLine();

                    if (comment.Id == id)
                    {
                        return comment;
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

        public List<Comment> GetAll(int TaskId)
        {
            List<Comment> result = new List<Comment>();

            FileStream fs = new FileStream(this.filePath, FileMode.OpenOrCreate);
            StreamReader sr = new StreamReader(fs);

            try
            {
                while (!sr.EndOfStream)
                {
                    Comment comment = new Comment();
                    comment.Id = Convert.ToInt32(sr.ReadLine());
                    comment.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    comment.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    comment.CommentBody = sr.ReadLine();

                    if (comment.ParentTaskId == TaskId)
                        result.Add(comment);


                }
            }
            finally
            {
                sr.Close();
                fs.Close();
            }

            return result;
        }

        public void Delete(Comment item)
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
                    Comment comment = new Comment();
                    comment.Id = Convert.ToInt32(sr.ReadLine());
                    comment.ParentTaskId = Convert.ToInt32(sr.ReadLine());
                    comment.ParentUserId = Convert.ToInt32(sr.ReadLine());
                    comment.CommentBody = sr.ReadLine();

                    if (comment.Id != item.Id)
                    {
                        sw.WriteLine(comment.Id);
                        sw.WriteLine(comment.ParentTaskId);
                        sw.WriteLine(comment.ParentUserId);
                        sw.WriteLine(comment.CommentBody);
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

        public void Save(Comment item)
        {
            if (item.Id > 0)
                Update(item);
            else
                Insert(item);
        }
    }
}