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
    [Authorize(Roles =
        AdminConstants.Roles.BoardMember + "," + AdminConstants.Roles.Administrator)]
    public class CommitteesController : Controller
    {
		private readonly ICommitteeRepository committeeRepository;
        private readonly IServerFileRepository serverFileRepository;
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

		// If you are using Dependency Injection, you can delete the following constructor
        public CommitteesController() : this(new CommitteeRepository(), new ServerFileRepository())
        {
        }

        public CommitteesController(ICommitteeRepository committeeRepository, IServerFileRepository serverFileRepository)
        {
			this.committeeRepository = committeeRepository;
            this.serverFileRepository = serverFileRepository;            
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
                c => c.AttachedFiles,
                c => c.Events));
        }

        //
        // GET: /Committees/Pages/5

        [AllowAnonymous]
        public ActionResult Pages(int id)
        {
            var committee = committeeRepository.Find(id);
            if (committee == null)
                return RedirectToAction("Index", "Home");

            committee.SortPosts();
            committee.SortEvents();
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
            HttpContext.Cache.Remove("PossibleChairs");
            return View(new Committee());
        } 

        //
        // POST: /Committees/Create

        [HttpPost]
        public ActionResult Create(Committee committee, string Command)
        {          
            return EditOrCreate(committee, Command);
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

        [Authorize(Roles =
            AdminConstants.Roles.CommitteeChair + "," +
            AdminConstants.Roles.BoardMember + "," +
            AdminConstants.Roles.Administrator)]
        public ActionResult Edit(int id)
        {
            HttpContext.Cache.Remove("PossibleChairs");
            var possibleChairs = PossibleChairs;
            ViewBag.PossibleChairs = ViewBagPossibleChairs(possibleChairs);

            var committee = committeeRepository.Find(id);

            CleanupCommitteeChairs(committee, possibleChairs);

            return View(committee);
        }

        /// <summary>
        /// This is done because the Admin controller did not always remove chairs from 
        /// committee when removing the chair role from a user. We may not need to 
        /// do it anymore but why not. 
        /// </summary>
        /// <param name="committee"></param>
        /// <param name="possibleChairs"></param>
        private void CleanupCommitteeChairs(Committee committee, List<ApplicationUser> possibleChairs)
        {
            var chairsToRemove = committee.ChairPersons
                .Where(chair => !possibleChairs.Any(p => p.MemberId == chair.MemberId))
                .ToList();

            if (chairsToRemove.Any())
            {
                chairsToRemove.ForEach(c =>
                {
                    committee.ChairPersons.Remove(c);
                    committeeRepository.RemoveChairPerson(c);
                });
                committeeRepository.Save();
            }
        }

        private void RemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            
        }

        private object ViewBagPossibleChairs(List<ApplicationUser> possibleChairs)
        {

            var cache = new System.Web.Caching.Cache();
            var viewBagPossibleChairs = HttpContext.Cache.Get("PossibleChairs");
            if (viewBagPossibleChairs == null)
            {
                viewBagPossibleChairs = possibleChairs;
                HttpContext.Cache.Insert("PossibleChairs", viewBagPossibleChairs);
            }

            return viewBagPossibleChairs;

        }

        private List<ApplicationUser> PossibleChairs
        {
            get
            {
                var userIds =
                        RoleManager
                        .FindByName(AdminConstants.Roles.CommitteeChair)
                        .Users
                        .Select(u => u.UserId)
                        .ToList();
                return UserManager.Users
                    .Where(u => userIds.Contains(u.Id))
                    .OrderBy(u => u.Member.Name.First)
                    .ToList();
            }
        }

        //
        // POST: /Committees/Edit/5

        [HttpPost]
        [Authorize(Roles =
            AdminConstants.Roles.CommitteeChair + "," +
            AdminConstants.Roles.BoardMember + "," +
            AdminConstants.Roles.Administrator)]
        public ActionResult Edit(Committee committee, string Command, HttpPostedFileBase File)
        {
            if (File != null)
            {
                var committeeFile = new CommitteeFile();
                committeeFile.CommitteeId = committee.CommitteeId;
                committeeFile.PostedFile = File;
                serverFileRepository.InsertOrUpdate(committeeFile, CurrentUser);
                serverFileRepository.Save();

                ViewBag.PossibleChairs = ViewBagPossibleChairs(PossibleChairs);
                return Edit(committee.CommitteeId);
            }

            return EditOrCreate(committee, Command);
        }

        private ActionResult EditOrCreate(Committee committee, string Command)
        {
            ViewBag.PossibleChairs = ViewBagPossibleChairs(PossibleChairs);

            switch (Command)
            {
                case "Add Committe Post":
                    return AddCommitteePost(committee);
                case "Add Committe Chair":
                    return AddCommitteeChair(committee);
                case "Add Committe Event":
                    return AddCommitteeEvent(committee);
            }

            if (ModelState.IsValid)
            {
                switch (Command)
                {
                    case "Save":
                        return Save(committee);
                    default:
                        return View(committee);
                }

            }
            else
            {
                return View(committee);
            }
        }

        private ActionResult Save(Committee committee)
        {
            // We dont save these via the committee.
            committee.AttachedFiles = null;
            committee.Posts = null;
            committee.Events = null;

            committeeRepository.InsertOrUpdate(committee, CurrentUser);
            committeeRepository.Save();
            return RedirectToAction("Index");
        }

        private ActionResult AddCommitteePost(Committee committee)
        {
            if (committee.Posts == null)
                committee.Posts = new List<CommitteePost>();

            return RedirectToAction("Create", "Posts", new {Type = "CommitteePost", CommitteeId = committee.CommitteeId});
        }

        private ActionResult AddCommitteeEvent(Committee committee)
        {
            if (committee.Events == null)
                committee.Events = new List<CommitteeEvent>();
            return RedirectToAction("Create", "Posts", new { Type = "CommitteeEvent", CommitteeId = committee.CommitteeId });
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

