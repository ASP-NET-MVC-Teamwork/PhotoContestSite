namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using InputModels;
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
                .OrderByDescending(c => c.CreatedOn)
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
                .To<ContestViewModel>()
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
                    OwnerId = this.UserProfile.Id

                };

                this.Data.Contests.Add(newContest);

                this.Data.SaveChanges();
                return this.RedirectToAction("Details", new { id = newContest.Id });
            }
            return this.View(contest);
        }

        public ActionResult Edit(int id)
        {
            var oldContest = this.Data.Contests.GetById(id);
            if (oldContest == null)
            {
                return this.HttpNotFound();
            }
            if (oldContest.OwnerId != this.UserProfile.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot edit a contest which is not yours.");
            }
            return View(new ContestViewModel()
            {
                Id = oldContest.Id,
                Title = oldContest.Title,
                Description = oldContest.Description,
                IsClosedForSubmissions = oldContest.IsClosedForSubmissions,
                IsClosedForVoting = oldContest.IsClosedForVoting
            });
        }

        public ActionResult Update(ContestViewModel model)
        {
            var contest = this.Data.Contests.GetById(model.Id);

            if (contest == null)
            {
                return this.HttpNotFound();
            }

            if (contest.OwnerId != this.UserProfile.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot edit a contest which is not yours.");
            }

            contest.Title = model.Title;
            contest.Description = model.Description;
            contest.IsClosedForSubmissions = model.IsClosedForSubmissions;
            contest.IsClosedForVoting = model.IsClosedForVoting;
            this.Data.SaveChanges();


            return this.RedirectToAction("Details", new { id = contest.Id });

        }

    }
}