namespace PhotoContest.Web.InputModels
{
    using System.Web;

    public class PictureInputModel
    {
        public string Title { get; set; }

        //public string Url { get; set; }
        
        public HttpPostedFileBase Photo { get; set; }
    }
}