

namespace PhotoContest.Data.UnitOfWork
{
    using Models;
    using Repositories;

    public interface IPhotoContestData
    {
        IRepository<Picture> Pictures { get; }
        IRepository<ApplicationUser> Users { get; }
    }
}
