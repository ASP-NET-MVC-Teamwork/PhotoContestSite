namespace PhotoContest.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Common.Mappings;
    using PhotoContest.Models;

    public class CommentViewModel: IMapFrom<Comment>
    {
        public int PictureId { get; set; }

        [Required(ErrorMessage = "Text cannot be empty.")]
        [MinLength(GlobalConstants.CommentMinLength)]
        [MaxLength(GlobalConstants.CommentMaxLength)]
        public string Text { get; set; }

        public int Id { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}