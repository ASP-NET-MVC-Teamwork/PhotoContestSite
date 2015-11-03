namespace PhotoContest.Data.Contracts
{
    using System.Linq;

    public interface IDeletableEntityRepository<T> : IRepository<T> where T : class
    {
        IQueryable<T> AllDeleted();

        void ActualDelete(T entity);
    }
}
