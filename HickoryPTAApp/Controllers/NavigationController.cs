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
        private readonly ICommitteeRepository committeeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public NavigationController() : this(new CommitteeRepository())
        {
            
        }

        public NavigationController(ICommitteeRepository committeeRepository)
        {
			this.committeeRepository = committeeRepository;
        }

        [ChildActionOnly]
        public ActionResult NavBar()
        {
            var navModel = new NavigationViewModel();
           
            navModel.NavigationCommittees.Add(new NavigationCommittee() { CommitteeId = 1, CommitteeName = "Test" });

            navModel.NavigationCommittees.AddRange(
                committeeRepository
                .All
                .Select(c => new NavigationCommittee()
                {
                    CommitteeId = c.CommitteeId,
                    CommitteeName = c.CommitteeName
                }));

            return PartialView("_NavBar", navModel);
        }
    }
}