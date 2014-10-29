using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTAData.Entities;
using HickoryPTAApp.Models;
using HickoryPTAApp.Extentions;

namespace HickoryPTAApp.Controllers
{
    [Authorize(Roles = 
        AdminConstants.Roles.CommitteeChair + "," + AdminConstants.Roles.Administrator)]
    public class PostsController : Controller
    {
        private readonly IPostRepository postRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public PostsController()
            : this(new PostRepository())
        {
        }

        public PostsController(IPostRepository postRepository)
        {
            this.postRepository = postRepository;
        }

        //
        // GET: /Posts/

        public ViewResult Index()
        {
            return View(postRepository.All);
        }

        public string CurrentUser
        {
            get
            {
                return User != null ? User.Identity.Name : "Anonymous";
            }
        }

        public ActionResult Create(string Type, string CommitteeId)
        {
            Type postType = Type == "CommitteePost" ? typeof(CommitteePost) : typeof(CommitteeEvent);
            var obj = Activator.CreateInstance(postType);
            var propInfo = postType.GetProperty("CommitteeId");
            propInfo.SetValue(obj, System.Convert.ToInt32(CommitteeId));
            return View(obj);
        }

        [HttpPost]
        public ActionResult Create(CommitteePost post, CommitteeEvent evt)
        {
            if (ModelState.IsValid)
            {
                if (evt.Location == null)
                    return Save(post);
                else
                    return Save(evt);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Posts/Details/5
        [AllowAnonymous]
        public ViewResult Details(int id)
        {
            return View(postRepository.Find(id));
        }

        //
        // GET: /Posts/Edit/5

        public ActionResult Edit(int id)
        {
            return View(postRepository.Find(id));
        }

        //
        // POST: /Posts/Edit/5

        [HttpPost]
        public ActionResult Edit(CommitteePost post, CommitteeEvent evt)
        {
            if (ModelState.IsValid)
            {
                if (evt.Location == null)
                    return Save(post);
                else
                    return Save(evt);
            }
            else
            {
                return View();
            }
        }

        private ActionResult Save(Post post)
        {
            postRepository.InsertOrUpdate(post, CurrentUser);
            postRepository.Save();
            var committeeId =
                post is CommitteeEvent ?
                (post as CommitteeEvent).CommitteeId :
                (post as CommitteePost).CommitteeId;
            return RedirectToAction("Pages", "Committees", new { id = committeeId });
        }


        //
        // GET: /Posts/Delete/5

        public ActionResult Delete(int id)
        {
            return View(postRepository.Find(id));
        }

        //
        // POST: /Posts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, string PreviousUrl)
        {
            postRepository.Delete(id);
            postRepository.Save();

            if (String.IsNullOrEmpty(PreviousUrl))
                return RedirectToAction("Index", "Home");
            else
                return Redirect(PreviousUrl);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                postRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

