namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Common;
    using Data.Contracts;
    using ViewModels;

    public class HomeController : BaseController
    {
        public HomeController(IPhotoContestData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                LatestContests = this.Data.Contests.All()
                    .OrderByDescending(c => c.CreatedOn)
                    .Take(GlobalConstants.NumberOfContestsOnHomePage)
                    .Project()
                    .To<ContestViewModel>()
            };

            return View(homeViewModel);
        }
    }
}