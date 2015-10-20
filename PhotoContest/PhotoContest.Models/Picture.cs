namespace PhotoContest.Models
{
    using System;
    using System.Collections.Generic;
    using Common;

    public class Picture : AuditInfo, IDeletableEntity
    {
        private ICollection<Vote> votes;
        private ICollection<Comment> comments; 

        public Picture()
        {
            this.CreatedOn = DateTime.Now;
            this.votes = new HashSet<Vote>();
            this.comments = new HashSet<Comment>();
        } 

        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
