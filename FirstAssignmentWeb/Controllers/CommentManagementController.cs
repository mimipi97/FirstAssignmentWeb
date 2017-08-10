using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Access.Entities;
using Data_Access.Repositories;
using FirstAssignmentWeb.Models;

namespace FirstAssignmentWeb.Controllers
{
    public class CommentManagementController : Controller
    {
        // GET: CommentManagement
        public ActionResult Index(int? ParentTaskId)
        {
            if (AuthenticationManager.LoggedUser == null && ParentTaskId == null)
                return RedirectToAction("Login", "Home");

            TaskRepository RepoTask = RepositoryFactory.GetTaskRepository();
            CommentRepository RepoComment = RepositoryFactory.GetCommentRepository();

            ViewData["task"] = RepoTask.GetById(ParentTaskId.Value);
            ViewData["comments"] = RepoComment.GetAll(ParentTaskId.Value);

            return View();
        }

        [HttpGet]
        public ActionResult EditComment(int? ParentTaskId, int? id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            CommentRepository RepoComment = RepositoryFactory.GetCommentRepository();

            Comment comment = null;
            if (id == null)
            {
                comment = new Comment();
                comment.ParentTaskId = ParentTaskId.Value;
                comment.ParentUserId = AuthenticationManager.LoggedUser.Id;
            }
            else
                comment = RepoComment.GetById(id.Value);

            ViewData["comment"] = comment;

            return View();
        }

        [HttpPost]
        public ActionResult EditComment(Comment comment)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            CommentRepository RepoComment = RepositoryFactory.GetCommentRepository();
            RepoComment.Save(comment);

            return RedirectToAction("Index", "CommentManagement", new { ParentTaskId = comment.ParentTaskId });
        }

        public ActionResult Delete(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            CommentRepository RepoComment = RepositoryFactory.GetCommentRepository();
            Comment comment = RepoComment.GetById(id);
            RepoComment.Delete(comment);

            return RedirectToAction("Index", "CommentManagement", new { ParentTaskId = comment.ParentTaskId });
        }
    }

}
