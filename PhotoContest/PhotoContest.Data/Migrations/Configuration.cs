namespace PhotoContest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualBasic.ApplicationServices;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotoContestDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhotoContestDbContext context)
        {
            SeedAdmin(context);
        }
        

        public void SeedAdmin(PhotoContestDbContext context)
        {

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var adminRole = new IdentityRole { Name = "Admin" };

                manager.Create(adminRole);
            }

            if (!context.Users.Any(u => u.UserName == "Admin"))
            {

                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var founder = new ApplicationUser()
                {
                     UserName= "Admin",
                     Email= "admin@admin.com",
                     JoinedOn = DateTime.Now,
                     IsDeleted = false

                };
                manager.Create(founder, "remzito");
                manager.AddToRole(founder.Id, "Admin");
            }
        }
    }
}
