namespace PhotoContest.Web.ViewModels
{
    public class LikeViewModel
    {
        public int Likes { get; set; }

        public int PictureId { get; set; }

        public string Action { get; set; }

        public string ThumbDirection { get; set; }
    }
}