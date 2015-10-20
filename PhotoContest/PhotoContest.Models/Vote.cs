namespace PhotoContest.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int PictureId { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
