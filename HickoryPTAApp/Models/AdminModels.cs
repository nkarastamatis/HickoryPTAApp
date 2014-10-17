﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HickoryPTAApp.Models
{
    public class AdminRolesViewModel
    {
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string NewRoleName { get; set; }
    }

    public class AdminUsersViewModel
    {
        public IEnumerable<IdentityUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public string SelectedUserId { get; set; }
    }
}