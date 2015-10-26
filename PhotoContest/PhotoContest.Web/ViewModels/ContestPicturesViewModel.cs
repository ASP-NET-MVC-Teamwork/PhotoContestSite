namespace PhotoContest.Web.ViewModels
{
    using System.Collections.Generic;

    public partial class ContestPicturesViewModel
    {
        public IEnumerable<PictureViewModel> Pictures { get; set; }

        public int ContestId { get; set; }
    }
}