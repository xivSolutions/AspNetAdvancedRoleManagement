using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AspNetRoleCustomization.Models;

namespace AspNetRoleCustomization.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var roles = db.ApplicationRoles.ToList();
            return View(db.ApplicationRoles.ToList());
        }


        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationrole = db.ApplicationRoles.Find(id);
            if (applicationrole == null)
            {
                return HttpNotFound();
            }
            return View(applicationrole);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Name,Description")] ApplicationRole applicationrole)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(applicationrole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationrole);
        }


        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationrole = db.ApplicationRoles.Find(id);
            if (applicationrole == null)
            {
                return HttpNotFound();
            }
            return View(applicationrole);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Name,Description")] ApplicationRole applicationrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicationrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicationrole);
        }


        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationRole applicationrole = db.ApplicationRoles.Find(id);
            if (applicationrole == null)
            {
                return HttpNotFound();
            }
            return View(applicationrole);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationRole applicationrole = db.ApplicationRoles.Find(id);
            db.Roles.Remove(applicationrole);
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
