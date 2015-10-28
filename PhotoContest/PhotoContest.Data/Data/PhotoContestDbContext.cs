namespace PhotoContest.Data.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using Contracts;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;
    using Models.Common;

    public class PhotoContestDbContext : IdentityDbContext<ApplicationUser>, IPhotoContestDbContext
    {
        public PhotoContestDbContext()
            : base("PhotoContest", false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotoContestDbContext, Configuration>());
        }

        public IDbSet<Contest> Contests { get; set; }

        public IDbSet<Picture> Pictures { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<Reward> Rewards { get; set; }

        public IDbSet<Vote> Votes { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static PhotoContestDbContext Create()
        {
            return new PhotoContestDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Contests)
                .WithMany(u => u.Participants)
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("ContestId");
                    x.ToTable("ContestsParticipants");
                });
        }

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
