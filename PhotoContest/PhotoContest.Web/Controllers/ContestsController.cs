namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using ViewModels;

    [Authorize]
    public class ContestsController : BaseController
    {
        public ContestsController(IPhotoContestData data) 
            : base(data)
        {
        }


        // GET: Contest
        [AllowAnonymous]
        public ActionResult Index()
        {
            var contests = this.Data.Contests
                .All()
                .OrderBy(c => c.CreatedOn)
                .Project()
                .To<ContestViewModel>();
                
            return View(contests);
        }

        //GET: Contests/Details/5
        public ActionResult Details(int id)
        {
            var contest = this.Data.Contests
                .All()
                .Where(x => x.Id == id)
                .Project()
                .To<ContestDetailsViewModel>()
                .FirstOrDefault();

            return this.View(contest);
        }
    }
}