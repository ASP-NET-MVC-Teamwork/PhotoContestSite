namespace PhotoContest.Data.Contracts
{
    using Models;

    public interface IPhotoContestData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Contest> Contests { get; }

        IRepository<Picture> Pictures { get; }

        IRepository<Comment> Comments { get; }

        IRepository<Reward> Rewards { get; }

        IRepository<Vote> Votes { get; }
    }
}
