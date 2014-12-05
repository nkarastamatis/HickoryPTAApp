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

            return PartialView("_FilesPartial", files);
        }
    }
}