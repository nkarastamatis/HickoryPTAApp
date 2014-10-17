using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PTAData.Entities;
using HickoryPTAApp.Models;

namespace HickoryPTAApp.Controllers
{
    public class NavigationController : Controller
    {
        [ChildActionOnly]
        public ActionResult NavBar()
        {
            var navModel = new NavigationViewModel();
            navModel.NavigationCommittees.Add(new NavigationCommittee() { CommitteeId = 1, CommitteeName = "Test" });

            return PartialView("_NavBar", navModel);
        }
    }
}