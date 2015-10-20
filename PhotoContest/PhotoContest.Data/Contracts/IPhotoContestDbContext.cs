namespace PhotoContest.Data.Contracts
{
    using System.Data.Entity;
    using Models;

    public interface IPhotoContestDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<Picture> Pictures { get; set; }

        int SaveChanges();
    }
}
