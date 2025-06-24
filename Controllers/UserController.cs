using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System.Linq;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User
        public ActionResult Index(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            var users = userlist.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(u => 
                    u.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                );
            }

            return View(users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            var user = userlist.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userlist.Count == 0)
                    {
                        user.Id = 1;
                    }
                    else
                    {
                        user.Id = userlist.Max(u => u.Id) + 1;
                    }
                    userlist.Add(user);
                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch
            {
                return View(user);
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var user = userlist.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = userlist.Find(u => u.Id == id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    existingUser.Name = user.Name;
                    existingUser.Email = user.Email;

                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch
            {
                return View(user);
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            var user = userlist.Find(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var user = userlist.Find(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                userlist.Remove(user);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
}
