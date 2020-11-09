using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EntityFrameworkCodeFirstDemo.Models;

namespace EntityFrameworkCodeFirstDemo.Controllers
{
    public class UsersController : Controller
    {
        private UserDBContext db = new UserDBContext();
        void InitilazieRoles()
        {
          
                Role role1 = new Role { id = 1, roleName = "User" };
                Role role2 = new Role { id = 2, roleName = "Manager" };
                Role role3 = new Role() { id = 3, roleName = "Admin" };
                db.roles.Add(role1); db.roles.Add(role2); db.roles.Add(role3);
                db.SaveChanges();
            
        }
        public UsersController()
    {
        
                if (db.roles.Count() == 0)
                {
                    InitilazieRoles();
                }
           
        }
       

        // GET: Users
        public ActionResult Index()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("TestCookie"))
            {
                HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["TestCookie"];

                ViewBag.CookieMessage = cookie.Value;
            }
            return View(db.users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.roles = FillRoles();
            ViewBag.Managers = GetManagers();
            return View();
        }

        List<SelectListItem> FillRoles()
        {
            List<SelectListItem> roleList = new List<SelectListItem>() {
            new SelectListItem
            {
                Text = "User",
                Value = "1"
            },
        new SelectListItem
        {
            Text = "Manager",
            Value = "2"
        },
        };
            return roleList;
        }

        List<SelectListItem> GetManagers()
        {
            //var query = from p in db.users
            //            join q in db.users on p.managerID equals q.id && 
            //            select new { p.firstName, q.lastName };
            var query =  from p in db.users
                         where p.roleID==2
                         select new {p.managerID, p.firstName, p.lastName };
            List<SelectListItem> ManagersList = new List<SelectListItem>();
            foreach (var element in query)
            {
                ManagersList.Add(new SelectListItem
                {
                    Value = element.managerID.ToString(),
                    Text = element.firstName.ToString() + " " + element.lastName.ToString()
                });
            }
            return ManagersList;
        }
        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
           

            if (ModelState.IsValid)
            {
                user.roleID = user.roleID;
                db.users.Add(user);
                db.SaveChanges();
                TempData["msg"] = "Record successfully inserted";
                return RedirectToAction("Index");
            }
            ViewBag.roles = FillRoles();
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userName,firstName,lastName,password,emailID,DateofJoin,managerID")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
