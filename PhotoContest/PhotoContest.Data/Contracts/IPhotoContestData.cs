namespace PhotoContest.Data.Contracts
{
    using Models;

    public interface IPhotoContestData
    {
        IRepository<Picture> Pictures { get; }
        IRepository<ApplicationUser> Users { get; }
    }
}
