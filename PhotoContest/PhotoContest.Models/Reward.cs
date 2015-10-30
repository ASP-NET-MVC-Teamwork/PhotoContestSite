namespace PhotoContest.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using PhotoContest.Common;

    public class Reward : IDeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.TitleMinLength)]
        [MaxLength(GlobalConstants.TitleMaxLength)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

    }
}
