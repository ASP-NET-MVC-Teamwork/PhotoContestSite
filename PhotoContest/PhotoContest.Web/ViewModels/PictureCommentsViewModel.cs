namespace PhotoContest.Web.ViewModels
{
    using System.Collections.Generic;

    public partial class PictureCommentsViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public int PictureId { get; set; }
    }
}