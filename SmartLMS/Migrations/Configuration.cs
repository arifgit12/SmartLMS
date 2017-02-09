namespace SmartLMS.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartLMS.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SmartLMS.Models.ApplicationDbContext context)
        {

            foreach (UserRoles role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!context.Roles.Any(r => r.Name == role.ToString()))
                {
                    var store = new RoleStore<IdentityRole>(context);
                    var manager = new RoleManager<IdentityRole>(store);
                    var newRole = new IdentityRole { Name = role.ToString() };
                    manager.Create(newRole);
                }
            }

            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    PhoneNumber = "9518557397"
                };

                manager.Create(user, "Admin@123");
                manager.AddToRole(user.Id, UserRoles.Administrator.ToString());
            }
            if (!context.Users.Any(u => u.UserName == "lecturer@admin.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "lecturer@admin.com",
                    Email = "lecturer@admin.com",
                    PhoneNumber = "9518557397"
                };

                manager.Create(user, "Abc@123");
                manager.AddToRole(user.Id, UserRoles.Lecturer.ToString());
            }
            if (!context.Users.Any(u => u.UserName == "student@admin.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    UserName = "student@admin.com",
                    Email = "student@admin.com",
                    PhoneNumber = "9518557397"
                };

                manager.Create(user, "Cde@123");
                manager.AddToRole(user.Id, UserRoles.User.ToString());
            }
        }
    }
}
