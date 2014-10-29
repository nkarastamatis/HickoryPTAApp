using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using PTAData.Entities;
using HickoryPTAApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;


namespace HickoryPTAApp.Controllers
{
    [Authorize(Roles =
        AdminConstants.Roles.BoardMember + "," + AdminConstants.Roles.Administrator)]
    public class MembersController : Controller
    {
		private readonly IMembershipRepository membershipRepository;
		private readonly IMemberRepository memberRepository;

        private ApplicationUserManager _userManager;

		// If you are using Dependency Injection, you can delete the following constructor
        public MembersController() : this(new MembershipRepository(), new MemberRepository())
        {
        }

        public MembersController(IMembershipRepository membershipRepository, IMemberRepository memberRepository)
        {
			this.membershipRepository = membershipRepository;
			this.memberRepository = memberRepository;
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

        //
        // GET: /Members/

        public ViewResult Index()
        {
            return View(memberRepository.AllIncluding(member => member.Membership));
        }

        //
        // GET: /Members/Details/5

        public ViewResult Details(int id)
        {
            return View(memberRepository.Find(id));
        }

        //
        // GET: /Members/Create

        public ActionResult Create()
        {
            ViewBag.PossibleMemberships = membershipRepository.All.AsEnumerable().ToList();
            var list = ((IEnumerable<PTAData.Entities.Membership>)ViewBag.PossibleMemberships);
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

                var membership = membershipRepository.Find(member.MembershipId);
                if (membership != null &&
                    membership.Address.StreetAddress == "PTA Chairs")
                {
                    AddUser(member);
                }

                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleMemberships = membershipRepository.All;
				return View();
			}
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        private ActionResult AddUser(Member member)
        {
            var user = new ApplicationUser { UserName = member.Email, Email = member.Email };
            IdentityResult result = new IdentityResult();
            try
            {
                user.MemberId = member.MemberId;
                result = UserManager.Create(user, "Hickory1");
                // below I was going to send an email to the users as the accounts were created
                // but for now I'm just going to return
                return null;
            }
            catch (Exception ex)
            {
                int x = 0;
            }
            if (result.Succeeded)
            {
                try
                {
                    var code = UserManager.GenerateEmailConfirmationToken(user.Id);
                    //var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action(
                       "ConfirmEmail", "Account",
                       new { userId = user.Id, code = code },
                       protocol: Request.Url.Scheme);

                    UserManager.SendEmail(user.Id,
                       "Confirm your account",
                       "Please confirm your account by clicking this link: <a href=\""
                                                       + callbackUrl + "\">link</a>");
                }
                catch (Exception ex)
                {
                    int i = 0;
                }
            }

            return null;
        }
        
        //
        // GET: /Members/Edit/5
 
        public ActionResult Edit(int id)
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
 
        public ActionResult Delete(int id)
        {
            return View(memberRepository.Find(id));
        }

        //
        // POST: /Members/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
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

