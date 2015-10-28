namespace PhotoContest.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class CommentInputModel
    {
        [Required]
        public string Text { get; set; }
    }
}