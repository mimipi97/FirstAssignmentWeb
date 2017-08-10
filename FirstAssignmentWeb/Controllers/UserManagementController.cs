using Data_Access.Entities;
using Data_Access.Repositories;
using FirstAssignmentWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class UserManagementController : Controller
    {
        public ActionResult Index()
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            UserRepository usersRepository = RepositoryFactory.GetUserRepository();
            ViewData["users"] = usersRepository.GetAll();

            return View();
        }

        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            UserRepository usersRepository = RepositoryFactory.GetUserRepository();

            User user = null;
            if (id == null)
                user = new User();
            else
                user = usersRepository.GetById(id.Value);

            ViewData["user"] = user;

            return View();
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            UserRepository usersRepository = RepositoryFactory.GetUserRepository();
            usersRepository.Save(user);

            return RedirectToAction("Index", "UserManagement");
        }

        public ActionResult DeleteUser(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
                return RedirectToAction("Login", "Home");

            UserRepository usersRepository = RepositoryFactory.GetUserRepository();
            User user = usersRepository.GetById(id);
            usersRepository.Delete(user);

            return RedirectToAction("Index", "UserManagement");
        }
    }
}