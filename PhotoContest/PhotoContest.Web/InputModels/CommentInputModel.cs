namespace PhotoContest.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using Common;

    public class CommentInputModel
    {
        [Required(ErrorMessage = "Comment cannot be empty.")]
        [MinLength(GlobalConstants.CommentMinLength)]
        [MaxLength(GlobalConstants.CommentMaxLength)]
        public string Text { get; set; }
    }
}