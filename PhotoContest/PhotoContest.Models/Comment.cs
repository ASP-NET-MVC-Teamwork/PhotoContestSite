namespace PhotoContest.Models
{
    using System;
    using Common;

    public class Comment : AuditInfo, IDeletableEntity
    {
        public Comment()
        {
            this.CreatedOn = DateTime.Now;
        }

        public int Id { get; set; }

        public int Text { get; set; }

        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
