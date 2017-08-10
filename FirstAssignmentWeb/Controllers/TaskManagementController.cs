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
    public class TaskManagementController : Controller
    {
        // GET: TaskManagement
        public ActionResult Index()
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            TaskRepository Repo = RepositoryFactory.GetTaskRepository();

            ViewData["tasks"] = Repo.GetAllYours(AuthenticationManager.LoggedUser.Id,AuthenticationManager.LoggedUser.Id);

            return View();
        }

        public ActionResult TaskDetails(int? id)
        {
            if (AuthenticationManager.LoggedUser == null && id == null)
            {
                return RedirectToAction("Index", "TaskManagement");
            }

            TaskRepository TaskRepository = RepositoryFactory.GetTaskRepository();
            
            ViewData["task"] = TaskRepository.GetById(id.Value);

            return View();
        }

        [HttpGet]
        public ActionResult EditTask(int? id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            TaskRepository Repo = RepositoryFactory.GetTaskRepository();
            UserRepository RepoUser = RepositoryFactory.GetUserRepository();
            
            Task item = null;
            if (id == null)
            {
                item = new Task();
            }
            else
            {
                item = Repo.GetById(id.Value);
            }


            if(item.AssigneeId == AuthenticationManager.LoggedUser.Id  && item.AssigneeId != AuthenticationManager.LoggedUser.Id)
            {
                return RedirectToAction("EditStatus", "TaskManagement", new { id = item.Id });
            }
            else
            {

                ViewData["task"] = item;
                ViewData["users"] = RepoUser.GetAll();
                return View();
            }         
        }

        [HttpPost]
        public ActionResult EditTask(Task task)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            TaskRepository Repo = RepositoryFactory.GetTaskRepository();

            task.RecentDate = DateTime.Now;

            if (task.Id == 0)
            {
                task.CreationDate = DateTime.Now;
                task.CreatorId = AuthenticationManager.LoggedUser.Id;
                Repo.Save(task);
                return RedirectToAction("Index", "TaskManagement");
            }
            else
            {
                Repo.Save(task);
                return RedirectToAction("EditComment", "CommentManagement", new { ParentTaskId = task.Id });
            }
        }

        [HttpGet]
        public ActionResult EditStatus(int? id)
        {
            if (AuthenticationManager.LoggedUser == null && id == null)
                return RedirectToAction("Login", "Home");

            TaskRepository Repo = RepositoryFactory.GetTaskRepository();
            
            ViewData["task"] = Repo.GetById(id.Value);
            return View();
        }

        [HttpPost]
        public ActionResult EditStatus(Task task)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            TaskRepository Repo = RepositoryFactory.GetTaskRepository();

            task.RecentDate = DateTime.Now;

            Repo.Save(task);
            return RedirectToAction("EditComment", "CommentManagement", new { ParentTaskId = task.Id });
        }

        public ActionResult Delete(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            TaskRepository Repo = RepositoryFactory.GetTaskRepository();

            Task task = Repo.GetById(id);
            Repo.Delete(task);

            return RedirectToAction("Index", "TaskManagement");
        }
    }
}