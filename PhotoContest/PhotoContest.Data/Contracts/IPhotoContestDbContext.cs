namespace PhotoContest.Data.Contracts
{
    using System.Data.Entity;
    using Models;

    public interface IPhotoContestDbContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        IDbSet<Contest> Contests { get; set; }

        IDbSet<Picture> Pictures { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Reward> Rewards { get; set; }

        IDbSet<Vote> Votes { get; set; }

    }
}
