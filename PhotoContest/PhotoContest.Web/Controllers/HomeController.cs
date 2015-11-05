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

        [AllowAnonymous]
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                LatestContests = this.Data.Contests
                    .All()
                    .OrderByDescending(c => c.CreatedOn)
                    .Take(GlobalConstants.NumberOfContestsOnHomePage)
                    .ProjectTo<ContestViewModel>(),
                LatestUsers = this.Data.Users.All()
                    .OrderByDescending(u => u.JoinedOn)
                    .Take(GlobalConstants.NumberOfUsersOnHomePage)
                    .ProjectTo<UserViewModel>()
            };

            return View(homeViewModel);
        }
    }
}