namespace PhotoContest.Web.ViewModels
{
    using Common.Mappings;
    using PhotoContest.Models;

    public class CommentViewModel: IMapFrom<Comment>
    {
        public int PictureId { get; set; }

        public string Text { get; set; }

        public int Id { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}