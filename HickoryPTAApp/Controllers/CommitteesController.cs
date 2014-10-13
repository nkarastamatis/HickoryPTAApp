using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTAData.Entities;
using HickoryPTAApp.Models;

namespace HickoryPTAApp.Controllers
{   
    public class CommitteesController : Controller
    {
		private readonly ICommitteeRepository committeeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public CommitteesController() : this(new CommitteeRepository())
        {
        }

        public CommitteesController(ICommitteeRepository committeeRepository)
        {
			this.committeeRepository = committeeRepository;
        }

        //
        // GET: /Committees/

        public ViewResult Index()
        {
            return View(committeeRepository.AllIncluding(
                c => c.ChairPersons,
                c => c.Posts,
                c => c.AttachedFiles));
        }

        //
        // GET: /Committees/Pages/5

        public ViewResult Pages(int id)
        {
            return View(committeeRepository.Find(id));
        }

        //
        // GET: /Committees/Details/5

        public ViewResult Details(int id)
        {
            return View(committeeRepository.Find(id));
        }

        //
        // GET: /Committees/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Committees/Create

        [HttpPost]
        public ActionResult Create(Committee committee)
        {
            if (ModelState.IsValid) {
                committee.UserModified = User != null ? User.Identity.Name : "Anonymous";
                committeeRepository.InsertOrUpdate(committee);
                committeeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Committees/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(committeeRepository.Find(id));
        }

        //
        // POST: /Committees/Edit/5

        [HttpPost]
        public ActionResult Edit(Committee committee)
        {
            if (ModelState.IsValid) {
                committeeRepository.InsertOrUpdate(committee);
                committeeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Committees/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(committeeRepository.Find(id));
        }

        //
        // POST: /Committees/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            committeeRepository.Delete(id);
            committeeRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                committeeRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

