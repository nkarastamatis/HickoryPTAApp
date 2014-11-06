using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HickoryPTAApp.Models;
using PTAData.Entities;

namespace HickoryPTAApp.Controllers
{
    [Authorize(Roles =
            AdminConstants.Roles.CommitteeChair + "," +
            AdminConstants.Roles.BoardMember + "," +
            AdminConstants.Roles.Administrator)]
    public class ServerFilesController : Controller
    {
        private HickoryPTAAppContext db = new HickoryPTAAppContext();

        // GET: ServerFiles
        public ActionResult Index()
        {
            return View(db.ServerFiles.ToList());
        }

        // GET: ServerFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServerFile serverFile = db.ServerFiles.Find(id);
            if (serverFile == null)
            {
                return HttpNotFound();
            }
            return View(serverFile);
        }

        // GET: ServerFiles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServerFiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FileId,FileName,Path,LastModified,CreatedOn,UserModified")] ServerFile serverFile)
        {
            if (ModelState.IsValid)
            {
                db.ServerFiles.Add(serverFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serverFile);
        }

        // GET: ServerFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServerFile serverFile = db.ServerFiles.Find(id);
            if (serverFile == null)
            {
                return HttpNotFound();
            }
            return View(serverFile);
        }

        // POST: ServerFiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FileId,FileName,Path,LastModified,CreatedOn,UserModified")] ServerFile serverFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serverFile).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serverFile);
        }

        // GET: ServerFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServerFile serverFile = db.ServerFiles.Find(id);
            if (serverFile == null)
            {
                return HttpNotFound();
            }
            return View(serverFile);
        }

        // POST: ServerFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string PreviousUrl)
        {
            ServerFile serverFile = db.ServerFiles.Find(id);
            db.ServerFiles.Remove(serverFile);
            var serverpath = Server.MapPath(serverFile.Path);
            System.IO.File.Delete(serverpath);
            db.SaveChanges();

            if (String.IsNullOrEmpty(PreviousUrl))
                return RedirectToAction("Index", "Home");
            else
                return Redirect(PreviousUrl);
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
