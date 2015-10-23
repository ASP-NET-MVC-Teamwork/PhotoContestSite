using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoContest.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Common.Mappings;
    using PhotoContest.Models;

    public class PictureViewModel : IMapFrom<Picture>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }


    }
}