using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.ViewModels
{
    public class PictureCommentsViewModel
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }

        public int PictureId { get; set; }
    }
}