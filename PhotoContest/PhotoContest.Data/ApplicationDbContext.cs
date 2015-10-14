using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PhotoContest.Data.Migrations;
using PhotoContest.Models;

namespace PhotoContest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public IDbSet<Image> Images { get; set; }
    }
}
