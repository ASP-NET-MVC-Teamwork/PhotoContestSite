namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using ImputModels;
    using Microsoft.AspNet.Identity;
    using PhotoContest.Models;
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

        public ActionResult Create()
        {
            var model = new ContestInputModel();
            return this.View(model);
        }

        [HttpPost]
        public ActionResult Create(ContestInputModel contest)
        {
            if (ModelState.IsValid)
            {
                var newContest = new Contest
                {
                    Title = contest.Title,
                    Description = contest.Description,
                    RewardStrategy = contest.RewardStrategy,
                    Type = contest.Type,
                    VotingStrategy = contest.VotingStrategy,
                    DeadlineStrategy = contest.DeadlineStrategy,
                    OwnerId = this.User.Identity.GetUserId()

                    //TODO: OWNER
                };

                this.Data.Contests.Add(newContest);

                this.Data.SaveChanges();
                return this.RedirectToAction("Details", new {id = newContest.Id});
            }
            return this.View(contest);

        }
    }
}