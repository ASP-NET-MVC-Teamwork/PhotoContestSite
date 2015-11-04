namespace PhotoContest.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Common.Filters;

    public class PictureInputModel
    {
        [Required(ErrorMessage = "Title cannot be empty!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Upload File")]
        [Display(Name = "Upload File")]
        [ValidateFile]
        public HttpPostedFileBase Photo { get; set; }
    }
}