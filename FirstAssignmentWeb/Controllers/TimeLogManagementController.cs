using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstAssignmentWeb.Models;
using Data_Access.Entities;
using Data_Access.Repositories;

namespace FirstAssignmentWeb.Controllers
{
    public class TimeLogManagementController : Controller
    {
        // GET: TimeLogManagement
        public ActionResult Index(int? ParentTaskId)
        {
            if (AuthenticationManager.LoggedUser == null && ParentTaskId == null)
                return RedirectToAction("Login", "Home");

            TaskRepository RepoTask = RepositoryFactory.GetTaskRepository();
            LogRepository RepoLogs = RepositoryFactory.GetLogRepository();

            ViewData["task"] = RepoTask.GetById(ParentTaskId.Value);
            ViewData["logs"] = RepoLogs.GetAll(ParentTaskId.Value);

            return View();
        }

        [HttpGet]
        public ActionResult EditLog(int? ParentTaskId, int? id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            LogRepository RepoLog = RepositoryFactory.GetLogRepository();

            TimeLog log = null;
            if (id == null)
            {
                log = new TimeLog();
                log.ParentTaskId = ParentTaskId.Value;
                log.ParentUserId = AuthenticationManager.LoggedUser.Id;
                log.CreationDate = DateTime.Now;
            }
            else
                log = RepoLog.GetById(id.Value);

            ViewData["log"] = log;

            return View();
        }

        [HttpPost]
        public ActionResult EditLog(TimeLog log)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            LogRepository RepoLog = RepositoryFactory.GetLogRepository();
            RepoLog.Save(log);

            return RedirectToAction("Index", "TimeLogManagement", new { ParentTaskId = log.ParentTaskId });
        }

        public ActionResult Delete(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            LogRepository RepoLog = RepositoryFactory.GetLogRepository();
            TimeLog log = RepoLog.GetById(id);
            RepoLog.Delete(log);

            return RedirectToAction("Index", "TimeLogManagement", new { ParentTaskId = log.ParentTaskId });
        }
    }

}

    