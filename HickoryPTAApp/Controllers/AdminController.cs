using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HickoryPTAApp.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace HickoryPTAApp.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
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

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Roles()
        {
            return View(new AdminRolesViewModel() { Roles = RoleManager.Roles });
        }

        [HttpPost]
        public async Task<ActionResult> Roles(AdminRolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(model.NewRoleName);
                var roleresult = await RoleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    ModelState.AddModelError("", roleresult.Errors.First().ToString());
                    return View(model);
                }

                model.NewRoleName = String.Empty;
                model.Roles = RoleManager.Roles;
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Users()
        {
            var model = new AdminUsersViewModel();
            GetUsersAndRoles(model);

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Users(AdminUsersViewModel model, string[] SelectedRoles)
        {
            if (SelectedRoles != null && !String.IsNullOrEmpty(model.SelectedUserId))
            {
                foreach (var selectedRole in SelectedRoles)
                {
                    {
                        if (!UserManager.IsInRole(model.SelectedUserId, selectedRole))
                            UserManager.AddToRole(model.SelectedUserId, selectedRole);
                    }
                }
            }

            GetUsersAndRoles(model);
            if (ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        private void GetUsersAndRoles(AdminUsersViewModel model)
        {
            model.Users = UserManager.Users.Include(u => u.Roles);
            model.Roles = RoleManager.Roles.ToList();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
