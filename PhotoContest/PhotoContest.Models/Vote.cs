namespace PhotoContest.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
