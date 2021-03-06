﻿namespace PhotoContest.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Common;
    using Common.Filters;

    public class PictureInputModel
    {
        [Required(ErrorMessage = "Title cannot be empty!")]
        [MaxLength(GlobalConstants.TitleMaxLength)]
        [MinLength(GlobalConstants.TitleMinLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Upload File!")]
        [ValidateFile]
        public HttpPostedFileBase Photo { get; set; }
    }
}