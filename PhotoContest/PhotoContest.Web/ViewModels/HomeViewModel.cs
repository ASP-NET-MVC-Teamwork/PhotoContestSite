namespace PhotoContest.Web.ViewModels
{
    using System.Collections.Generic;

    public class HomeViewModel
    {
        public IEnumerable<ContestViewModel> LatestContests { get; set; }
    }
}