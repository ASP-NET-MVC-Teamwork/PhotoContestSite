namespace PhotoContest.Web.ViewModels
{
    using System.Collections.Generic;
    using Common.Mappings;
    using PhotoContest.Models;

    public class PictureViewModel : IMapFrom<Picture>
    {
        public int PictureId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public int ContestId { get; set; }

        public int VotesCount { get; set; }
    }
}