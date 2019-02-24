using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using URent.Models;

namespace URent.Controllers
{
    public class SUPUsersController : Controller
    {
        private SUPContext db = new SUPContext();

        [Authorize]
        private string getIdentityID()
        {
            return User.Identity.GetUserId();
        }

        [Authorize]
        private int getSUPUserID()
        {
            string id = getIdentityID();
            SUPUser supUser = db.SUPUsers.Where(u => u.NetUserId.Equals(id)).FirstOrDefault();
            int supUserid = supUser.Id;
            return supUserid;
        }

        // GET: SUPUsers
        //public ActionResult Index()
        //{
        //    return View(db.SUPUsers.ToList());
        //}

        // GET: SUPUsers/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            SUPUser sUPUser = db.SUPUsers.Find(id);
            if (User.Identity.IsAuthenticated)
            {
                sUPUser = db.SUPUsers.Find(getSUPUserID());
                return View(sUPUser);
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (sUPUser == null)
            {
                return HttpNotFound();
            }
            return View(sUPUser);
        }

        //// GET: SUPUsers/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: SUPUsers/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,FirstName,LastName,DateOfBirth,StreetAddress,CityAddress,StateAddress,ZipCode,TimeStamp,NetUserId")] SUPUser sUPUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.SUPUsers.Add(sUPUser);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(sUPUser);
        //}

        // GET: SUPUsers/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPUser sUPUser = db.SUPUsers.Find(id);
            if (sUPUser == null)
            {
                return HttpNotFound();
            }
            if(id == getSUPUserID())
            {
                return View(sUPUser);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // POST: SUPUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,DateOfBirth,StreetAddress,CityAddress,StateAddress,ZipCode,TimeStamp,NetUserId")] SUPUser sUPUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUPUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new {id = sUPUser.Id });
            }
            return View(sUPUser);
        }

        // GET: SUPUsers/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPUser sUPUser = db.SUPUsers.Find(id);
            if (sUPUser == null)
            {
                return HttpNotFound();
            }
            return View(sUPUser);
        }

        // POST: SUPUsers/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SUPUser sUPUser = db.SUPUsers.Find(id);
            db.SUPUsers.Remove(sUPUser);
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

        [Authorize]
        public ActionResult Error()
        {
            return View();
        }
    }
}
