using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HickoryPTAApp.Controllers;

namespace HickoryPTAApp.Extentions
{
    public static class ControllerExtentions
    {
        public static string CurrentUser(this CommitteesController c)
        {
            return c.User != null ? c.User.Identity.Name : "Anonymous";
        }
    }
}