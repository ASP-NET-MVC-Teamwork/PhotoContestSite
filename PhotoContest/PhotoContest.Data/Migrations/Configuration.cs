namespace PhotoContest.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotoContest.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            //TODO: Remove in production
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PhotoContest.Data.ApplicationDbContext context)
        {
           
        }
    }
}
