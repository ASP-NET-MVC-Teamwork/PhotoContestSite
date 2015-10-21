namespace PhotoContest.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Comment : AuditInfo, IDeletableEntity
    {
        public Comment()
        {
            this.CreatedOn = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
