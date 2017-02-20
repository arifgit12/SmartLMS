using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLMS.Areas.Admin.Models;
using SmartLMS.Controllers;
using SmartLMS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SmartLMS.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UsersController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        protected ISmartLMSData Data { get; private set; }
        public UsersController(ApplicationUserManager userManager, RoleManager<IdentityRole> roleManager, ISmartLMSData data) 
            : base(userManager)
        {
            this._roleManager = roleManager;
            this.Data = data;
        }

        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = this.Data.Users.All().ToList();
            return View(users);
        }

        [HttpGet]
        [Route("admin/users/roles/")]
        public async Task<ActionResult> Roles(string id)
        {
            EditUserModel um = new EditUserModel();
            um.User = this.Data.Users.SearchFor( u => u.Id == id).SingleOrDefault();
            um.Roles = await UserManager.GetRolesAsync(um.User.Id);
            return View(um);
        }
    }
}