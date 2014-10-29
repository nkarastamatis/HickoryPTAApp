using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTAData.Entities;
using HickoryPTAApp.Models;

namespace HickoryPTAApp.Controllers
{
    [Authorize(Roles = AdminConstants.Roles.Administrator)]
    public class MembershipsController : Controller
    {
		private readonly IMembershipRepository membershipRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public MembershipsController() : this(new MembershipRepository())
        {
        }

        public MembershipsController(IMembershipRepository membershipRepository)
        {
			this.membershipRepository = membershipRepository;
        }

        //
        // GET: /Memberships/

        public ViewResult Index()
        {
            return View(membershipRepository.AllIncluding(membership => membership.Members, membership => membership.Students));
        }

        //
        // GET: /Memberships/Details/5

        public ViewResult Details(int id)
        {
            return View(membershipRepository.Find(id));
        }

        //
        // GET: /Memberships/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Memberships/Create

        [HttpPost]
        public ActionResult Create(Membership membership)
        {
            if (ModelState.IsValid) {
                membershipRepository.InsertOrUpdate(membership);
                membershipRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Memberships/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(membershipRepository.Find(id));
        }

        //
        // POST: /Memberships/Edit/5

        [HttpPost]
        public ActionResult Edit(Membership membership)
        {
            if (ModelState.IsValid) {
                membershipRepository.InsertOrUpdate(membership);
                membershipRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Memberships/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(membershipRepository.Find(id));
        }

        //
        // POST: /Memberships/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            membershipRepository.Delete(id);
            membershipRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                membershipRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

