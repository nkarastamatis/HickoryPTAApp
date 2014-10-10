using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTAData.Entities;
using HickoryPTAApp.Models;

namespace HickoryPTAApp.Controllers
{   
    public class MembersController : Controller
    {
		private readonly IMembershipRepository membershipRepository;
		private readonly IMemberRepository memberRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public MembersController() : this(new MembershipRepository(), new MemberRepository())
        {
        }

        public MembersController(IMembershipRepository membershipRepository, IMemberRepository memberRepository)
        {
			this.membershipRepository = membershipRepository;
			this.memberRepository = memberRepository;
        }

        //
        // GET: /Members/

        public ViewResult Index()
        {
            return View(memberRepository.AllIncluding(member => member.Membership));
        }

        //
        // GET: /Members/Details/5

        public ViewResult Details(string id)
        {
            return View(memberRepository.Find(id));
        }

        //
        // GET: /Members/Create

        public ActionResult Create()
        {
			ViewBag.PossibleMemberships = membershipRepository.All;
            return View();
        } 

        //
        // POST: /Members/Create

        [HttpPost]
        public ActionResult Create(Member member)
        {
            if (ModelState.IsValid) {
                memberRepository.InsertOrUpdate(member);
                memberRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleMemberships = membershipRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Members/Edit/5
 
        public ActionResult Edit(string id)
        {
			ViewBag.PossibleMemberships = membershipRepository.All;
             return View(memberRepository.Find(id));
        }

        //
        // POST: /Members/Edit/5

        [HttpPost]
        public ActionResult Edit(Member member)
        {
            if (ModelState.IsValid) {
                memberRepository.InsertOrUpdate(member);
                memberRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleMemberships = membershipRepository.All;
				return View();
			}
        }

        //
        // GET: /Members/Delete/5
 
        public ActionResult Delete(string id)
        {
            return View(memberRepository.Find(id));
        }

        //
        // POST: /Members/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            memberRepository.Delete(id);
            memberRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                membershipRepository.Dispose();
                memberRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

