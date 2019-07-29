using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AdminAPIAdmiss.DataBase;
using AdminAPIAdmiss.Models;
using Microsoft.AspNet.Identity;
using AdminAPIAdmiss.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

namespace AdminAPIAdmiss.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AIndex()
        {
            var items = User.Identity.GetUserId();
            var users = db.Users;

            return View(users);
        }

        // GET: Admin/Details/5
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var person = db.Users.Find(id);

            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompanyId, Email")] ApplicationUser person)
        {
            if (ModelState.IsValid)
            {
                person.Id = new Random().Next(1000, 9999).ToString();
                //person. = DateTime.Now;
                db.Users.Add(person);
                db.SaveChanges();
                return RedirectToAction("AIndex");
            }
            return View(person);

            //var userContext = new ApplicationDbContext();
            //var user = UserManager.Users.SingleOrDefault(u => u.Id == id);

            //var userStore = new UserStore<User>(userContext);

            //await UserManager.DeleteAsync(user);

            //// var userManager = new UserManager<User>(userStore);

            //// await userManager.DeleteAsync(user);

            //return RedirectToAction("Index");
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var person = db.Users.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: Admin/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([Bind(Include = "Id,CompanyId,Email,AboutUs,CompanyInfo,EndData,SecretNumberCode")] ApplicationUser person)
        {
            if (ModelState.IsValid)
            {

                db.Entry(person).State = EntityState.Modified;
                //person.StartData = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("AIndex");
            }
            return View(person);
        }

        public ActionResult ADelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser person = db.Users.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
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
