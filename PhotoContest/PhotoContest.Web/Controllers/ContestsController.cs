namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Common;
    using Data.Contracts;
    using InputModels;
    using PhotoContest.Models;
    using ViewModels;
    using PagedList;

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
                .Where(c => c.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<ContestViewModel>()
                .ToList();

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

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ContestInputModel();
            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: /Contests/Delete/5
        [ChildActionOnly]
        public ActionResult Delete(int contestId)
        {
            Contest contest = this.Data.Contests.GetById(contestId);

            if (contest == null)
            {
                return HttpNotFound();
            }

            if (contest.Owner != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot delete a contest which is not yours.");
            }

            return View(new ContestViewModel()
            {
                Id = contest.Id,
                CreatedOn = contest.CreatedOn,
                DeadlineStrategy = contest.DeadlineStrategy,
                Description = contest.Description,
                IsClosedForVoting = contest.IsClosedForVoting,
                IsClosedForSubmissions = contest.IsClosedForSubmissions,
                Owner = contest.Owner,
                RewardStrategy = contest.RewardStrategy,
                Title = contest.Title,
                Type = contest.Type,
                VotingStrategy = contest.VotingStrategy
            });
        }

        // POST: /Contests/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ContestViewModel model)
        {
            Contest contest = this.Data.Contests.GetById(model.Id);

            if (contest == null)
            {
                return HttpNotFound();
            }

            if (contest.Owner != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot delete a contest which is not yours.");
            }

            this.Data.Contests.Delete(contest);

            this.Data.SaveChanges();

            return RedirectToAction("Index", "Contests", new { id = model.Id });
        }

        [HttpGet]
        public PartialViewResult GetDeletePartial(int id)
        {
            Contest contest = this.Data.Contests.GetById(id);

            return PartialView("Delete", new ContestViewModel()
            {
                Id = contest.Id,
                CreatedOn = contest.CreatedOn,
                DeadlineStrategy = contest.DeadlineStrategy,
                Description = contest.Description,
                IsClosedForVoting = contest.IsClosedForVoting,
                IsClosedForSubmissions = contest.IsClosedForSubmissions,
                Owner = contest.Owner,
                RewardStrategy = contest.RewardStrategy,
                Title = contest.Title,
                Type = contest.Type,
                VotingStrategy = contest.VotingStrategy
            });
        }

        [HttpGet, Authorize]
        public ActionResult MyContests(int? page)
        {
            var userId = this.UserProfile.Id;

            var myContests = this.Data.Contests
                .All()
                .Where(c => c.OwnerId == userId)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<ContestViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.PartialView("Partial/_MyContests", myContests);
        }

        [HttpGet]
        public ActionResult ArchivedContests(int? page)
        {
            var archivedContests = this.Data.Contests
                .All()
                .Where(c => c.IsDeleted == true)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<ContestViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.PartialView("Partial/_ArchivedContests", archivedContests);
        }

        [HttpGet]
        public ActionResult ActiveContests(int? page)
        {
            var activeContests = this.Data.Contests
                .All()
                .Where(c => c.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<ContestViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.PartialView("Partial/_ActiveContests", activeContests);
        }

        [HttpGet]
        public ActionResult AllContests(int? page)
        {
            var allContests = this.Data.Contests
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<ContestViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.PartialView("Partial/_AllContests", allContests);
        }

    }
}