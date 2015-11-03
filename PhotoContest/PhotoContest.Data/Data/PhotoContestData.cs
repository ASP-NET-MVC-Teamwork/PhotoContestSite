namespace PhotoContest.Data.Data
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Models;
    using Models.Common;

    public class PhotoContestData : IPhotoContestData
    {
        private IPhotoContestDbContext context;
        private readonly IDictionary<Type, object> repositories;

        public PhotoContestData(IPhotoContestDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }
        
        public IDeletableEntityRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        public IDeletableEntityRepository<Contest> Contests
        {
            get { return this.GetRepository<Contest>(); }
        }

        public IDeletableEntityRepository<Picture> Pictures
        {
            get { return this.GetRepository<Picture>(); }
        }

        public IDeletableEntityRepository<Comment> Comments
        {
            get { return this.GetRepository<Comment>(); }
        }

        public IDeletableEntityRepository<Reward> Rewards
        {
            get { return this.GetRepository<Reward>(); }
        }

        public IDeletableEntityRepository<Vote> Votes
        {
            get { return this.GetRepository<Vote>(); }
        }

        public IDeletableEntityRepository<Notification> Notifications
        {
            get { return this.GetRepository<Notification>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IDeletableEntityRepository<T> GetRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
