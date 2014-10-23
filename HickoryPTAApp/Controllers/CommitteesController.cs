using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PTAData.Entities;
using HickoryPTAApp.Models;
using HickoryPTAApp.Extentions;
using System.Web.Caching;

namespace HickoryPTAApp.Controllers
{
    [Authorize(Roles = AdminConstants.Roles.Administrator)]
    public class CommitteesController : Controller
    {
		private readonly ICommitteeRepository committeeRepository;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

		// If you are using Dependency Injection, you can delete the following constructor
        public CommitteesController() : this(new CommitteeRepository())
        {
        }

        public CommitteesController(ICommitteeRepository committeeRepository)
        {
			this.committeeRepository = committeeRepository;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? ApplicationRoleManager.Create();
            }
            private set
            {
                _roleManager = value;
            }
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

        [AllowAnonymous]
        public ActionResult Pages(int id)
        {
            var committee = committeeRepository.Find(id);
            if (committee == null)
                return RedirectToAction("Index", "Home");
            return View(committee);
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
                committeeRepository.InsertOrUpdate(committee, CurrentUser);
                committeeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        public string CurrentUser
        {
            get
            {
                return User != null ? User.Identity.Name : "Anonymous";
            }
        }

        //
        // GET: /Committees/Edit/5
 
        public ActionResult Edit(int id)
        {
            var userIds = 
                RoleManager
                .FindByName(AdminConstants.Roles.CommitteeChair)
                .Users
                .Select(u => u.UserId)
                .ToList();
            var possibleChairs = UserManager.Users.Where(u => userIds.Contains(u.Id)).ToList();
            ViewBag.PossibleChairs = possibleChairs;
            HttpContext.Cache.Insert("PossibleChairs", possibleChairs);

            return View(committeeRepository.Find(id));
        }

        private void RemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            
        }

        //
        // POST: /Committees/Edit/5

        [HttpPost]
        public ActionResult Edit(Committee committee, string Command)
        {
            var cache = new System.Web.Caching.Cache();
            ViewBag.PossibleChairs = HttpContext.Cache.Get("PossibleChairs");
            if (ModelState.IsValid) {
                switch (Command)
                {
                    case "Save":
                        return Save(committee);
                    case "Add Committe Post":
                        return AddCommitteePost(committee);
                    case "Add Committe Chair":
                        return AddCommitteeChair(committee);
                    case "Add Committe Event":
                        return AddCommitteeEvent(committee);
                    default:
                        return View();
                }
                
            } else {
				return View();
			}
        }

        private ActionResult Save(Committee committee)
        {
            committeeRepository.InsertOrUpdate(committee, CurrentUser);
            committeeRepository.Save();
            return RedirectToAction("Index");
        }

        private ActionResult AddCommitteePost(Committee committee)
        {
            if (committee.Posts == null)
                committee.Posts = new List<CommitteePost>();
            committee.Posts.Add(new CommitteePost());
            return View(committee);
        }

        private ActionResult AddCommitteeEvent(Committee committee)
        {
            if (committee.Events == null)
                committee.Events = new List<CommitteeEvent>();
            committee.Events.Add(new CommitteeEvent() { EventDate = DateTime.Now });
            return View(committee);
        }

        private ActionResult AddCommitteeChair(Committee committee)
        {
            if (committee.ChairPersons == null)
                committee.ChairPersons = new List<ChairPerson>();
            committee.ChairPersons.Add(new ChairPerson());
            return View(committee);
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

        public System.Web.Caching.CacheItemRemovedCallback onRemove { get; set; }
    }
}

