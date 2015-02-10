using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTAData.Entities;
using System.Data.Entity;

namespace HickoryPTAApp.Controllers
{
    //[RequireHttps]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Events()
        {
            var events = new List<CommitteeEvent>();

            using (var context = new HickoryPTAApp.Models.HickoryPTAAppContext())
            {
                IQueryable<CommitteeEvent> query = context.CommitteeEvents;
                events.AddRange(
                    query
                    .Include(e => e.Committee)
                    .Include(e => e.Location)
                    .OrderBy(e => e.EventDate)
                    .Where(e => e.EventDate >= DateTime.Today)
                    .Take(10)
                    .ToList());
            }            

            return PartialView("_EventsPartial", events);
        }

        [ChildActionOnly]
        public ActionResult Files()
        {
            var files = new List<CommitteeFile>();

            using (var committeeRepo = new HickoryPTAApp.Models.CommitteeRepository())
            {
                var globalPtaCommittee = committeeRepo.GlobalPtaCommittee();
                files.AddRange(globalPtaCommittee.AttachedFiles.ToList());
            }

            return FilesPartialView(files.ToList<ServerFile>(), "Important Documents");
        }

        [ChildActionOnly]
        public ActionResult Newsletters()
        {
            var files = new List<CommitteeFile>();

            using (var committeeRepo = new HickoryPTAApp.Models.CommitteeRepository())
            {
                var globalNewsletterCommittee = committeeRepo.GlobalNewsletterCommittee();
                if (globalNewsletterCommittee != null)
                    files.AddRange(globalNewsletterCommittee.AttachedFiles.ToList());
            }

            return FilesPartialView(files.ToList<ServerFile>(), "Newsletters");
        }

        [ChildActionOnly]
        public ActionResult RecentUploads()
        {
            var files = new List<ServerFile>();
            var recentUploadsCutoffDate = DateTime.Today.AddDays(-30);
            using (var context = new HickoryPTAApp.Models.HickoryPTAAppContext())
            {
                IQueryable<ServerFile> query = context.ServerFiles;
                files.AddRange(
                    query
                    .Where(p => p.LastModified >= recentUploadsCutoffDate)
                    .OrderByDescending(p => p.LastModified)
                    .Take(20)
                    .ToList());
            }

            return FilesPartialView(files, "New Files");
        }

        private ActionResult FilesPartialView(List<ServerFile> files, string title)
        {
            var model = new Models.FilesPartialModel();
            model.Files = files;
            model.Title = title;

            return PartialView("_FilesPartial", model);
        }

        [ChildActionOnly]
        public ActionResult BreakingNews()
        {
            var posts = new List<CommitteePost>();
            var breakingNewsCutoffDate = DateTime.Today.AddDays(-10);
            using (var context = new HickoryPTAApp.Models.HickoryPTAAppContext())
            {
                IQueryable<CommitteePost> query = context.CommitteePosts;
                posts.AddRange(
                    query
                    .Include(p => p.Committee)
                    .Include(p => p.Files)
                    .Where(p => p.LastModified >= breakingNewsCutoffDate)
                    .OrderByDescending(p => p.LastModified)
                    .ToList());
            }

            return PartialView("_BreakingNewsPartial", posts);
        }
    }
}