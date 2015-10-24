﻿namespace PhotoContest.Web.ViewModels
{
    using Common.Mappings;
    using PhotoContest.Models;

    public class PictureViewModel : IMapFrom<Picture>
    {
        public string PictureId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
    }
}