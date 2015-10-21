namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using ViewModel;

    public class ContestsController : BaseController
    {
        public ContestsController(IPhotoContestData data) : base(data)
        {
        }
        // GET: Contest
        public ActionResult Index()
        {
            var contests = this.Data.Contests
                .All()
                .OrderBy(c => c.CreatedOn)
                .Project()
                .To<ContestViewModel>()
                .ToList();
                
            return View(contests);
        }
    }
}