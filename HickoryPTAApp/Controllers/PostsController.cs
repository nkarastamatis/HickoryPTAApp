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

        //
        // GET: /Posts/Edit/5

        public ActionResult Edit(int id)
        {
            return View(postRepository.Find(id));
        }

        //
        // POST: /Posts/Edit/5

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                return Save(post);
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
            return RedirectToAction("Index");
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
        public ActionResult DeleteConfirmed(int id)
        {
            postRepository.Delete(id);
            postRepository.Save();

            return RedirectToAction("Index");
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

