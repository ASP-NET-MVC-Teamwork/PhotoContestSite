namespace PhotoContest.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models.Common;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;

    public class PhotoContestDbContext : IdentityDbContext<ApplicationUser>, IPhotoContestDbContext
    {
        public PhotoContestDbContext()
            : base("PhotoContestDbContext", false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoContestDbContext, Configuration>());
        }

        public static PhotoContestDbContext Create()
        {
            return new PhotoContestDbContext();
        }

        public IDbSet<Picture> Pictures { get; set; }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
