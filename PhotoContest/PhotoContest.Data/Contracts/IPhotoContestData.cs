namespace PhotoContest.Data.Contracts
{
    using Models;

    public interface IPhotoContestData
    {
        IDeletableEntityRepository<ApplicationUser> Users { get; }

        IDeletableEntityRepository<Contest> Contests { get; }

        IDeletableEntityRepository<Picture> Pictures { get; }

        IDeletableEntityRepository<Comment> Comments { get; }

        IDeletableEntityRepository<Reward> Rewards { get; }

        IDeletableEntityRepository<Vote> Votes { get; }

        IDeletableEntityRepository<Notification> Notifications { get; }

        int SaveChanges();
    }
}
