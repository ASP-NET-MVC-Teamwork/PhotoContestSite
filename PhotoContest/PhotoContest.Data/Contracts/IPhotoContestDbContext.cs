namespace PhotoContest.Data.Contracts
{
    using System.Data.Entity;
    using Models;

    public interface IPhotoContestDbContext
    {
        IDbSet<ApplicationUser> Users { get; }

        IDbSet<Contest> Contests { get; }

        IDbSet<Picture> Pictures { get; }

        IDbSet<Comment> Comments { get; }

        IDbSet<Reward> Rewards { get; }

        IDbSet<Vote> Votes { get; }

        IDbSet<Notification> Notifications { get; }

        int SaveChanges();
    }
}
