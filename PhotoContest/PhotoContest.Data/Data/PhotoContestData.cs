namespace PhotoContest.Data.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Models;

    public class PhotoContestData : IPhotoContestData
    {
        private IPhotoContestDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public PhotoContestData(IPhotoContestDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }
        
        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IRepository<Contest> Contests
        {
            get { return this.GetRepository<Contest>(); }
        }

        public IRepository<Picture> Pictures
        {
            get { return this.GetRepository<Picture>(); }
        }

        public IRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IRepository<Reward> Rewards
        {
            get { return this.GetRepository<Reward>(); }
        }

        public IRepository<Vote> Votes
        {
            get { return this.GetRepository<Vote>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
