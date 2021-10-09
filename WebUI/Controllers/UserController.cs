using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SBL;
using Models;
namespace WebUI.Controllers
{
    public class UserController : Controller
    {
        private IBL _bl;
        public UserController(IBL bl)
        {
            _bl = bl;
        }

        
        // GET: UserController
        
        public ActionResult Index()
        {
            if (Request.Cookies["CurrentUserId"] != null)
            {
                if (Request.Cookies["CurrentUserAccess"] == "True")
                {
                    List<User> allUsers = _bl.GetAllUsers();
                    return View(allUsers);
                }
                else
                {
                    return RedirectToAction("Profile", Convert.ToInt32(Request.Cookies["CurrentUserId"]));
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: UserController/Profile/5
        public ActionResult Profile(int id)
        {
            if (Request.Cookies["CurrentUserId"] != null)
            {
                User currentUser = _bl.GetUserById(Convert.ToInt32(Request.Cookies["CurrentUserId"]));
                return View(currentUser);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _bl.AddUser(user);
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            if (Request.Cookies["CurrentUserId"] != null)
            {
                User currentUser = _bl.GetUserById(Convert.ToInt32(Request.Cookies["CurrentUserId"]));
                if (currentUser.Access == true)
                {
                    User targetUser = _bl.GetUserById(id);

                    return View(targetUser);
                }
                else return View(currentUser);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            List<User> allUsers = _bl.GetAllUsers();
            for (int x = 0; x< allUsers.Count(); x++)
            {
                if (email.ToLower() == allUsers[x].Email.ToLower() && password == allUsers[x].Password)
                {
                    Response.Cookies.Append("CurrentUserId", allUsers[x].Id.ToString());
                    Response.Cookies.Append("CurrentUserName", allUsers[x].Name);
                    Response.Cookies.Append("CurrentUserAccess", allUsers[x].Access.ToString());
                    return RedirectToAction("Profile", allUsers[x].Id);
                }
            }
            return RedirectToAction("Login");
        }

        
    }
}
