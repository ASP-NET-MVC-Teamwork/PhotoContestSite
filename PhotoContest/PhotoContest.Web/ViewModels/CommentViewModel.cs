using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.ViewModels
{
    using Common.Mappings;
    using PhotoContest.Models;

    public class CommentViewModel: IMapFrom<Comment>
    {
        public int PictureId { get; set; }

        public string Text { get; set; }

        public int CommentId { get; set; }
    }
}