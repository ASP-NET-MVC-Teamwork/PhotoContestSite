namespace PhotoContest.Common.Filters
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int MaxContentLength = 1024 * 1024 * 2; //2 MB
            string[] allowedFileExtensions = { ".jpg", ".gif", ".png", ".jpeg" };

            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                return false;
            }
            else if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                ErrorMessage = "You can upload only photo of type: " + string.Join(", ", allowedFileExtensions);
                return false;
            }
            else if (file.ContentLength > MaxContentLength)
            {
                ErrorMessage = "Your Photo is too large, maximum allowed size is: " + (MaxContentLength / 1024).ToString() + "MB";
                return false;
            }
            else { 
                return true;
            }
        }
    }
}
