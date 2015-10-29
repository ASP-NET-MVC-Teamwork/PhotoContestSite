namespace PhotoContest.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class Vote: IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
