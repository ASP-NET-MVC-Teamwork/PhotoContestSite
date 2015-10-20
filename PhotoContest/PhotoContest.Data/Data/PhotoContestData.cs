namespace PhotoContest.Data.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Contracts;
    using Models;

    public class PhotoContestData : IPhotoContestData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        public PhotoContestData()
            : this(new PhotoContestDbContext())
        {
        }

        public PhotoContestData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Picture> Pictures
        {
            get { return this.GetRepository<Picture>(); }
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this.dbContext));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
