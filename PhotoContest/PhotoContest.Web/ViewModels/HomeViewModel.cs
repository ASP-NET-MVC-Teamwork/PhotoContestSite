namespace PhotoContest.Web.ViewModels
{
    using System.Collections.Generic;

    public partial class HomeViewModel
    {
        public IEnumerable<ContestViewModel> LatestContests { get; set; }
    }
}