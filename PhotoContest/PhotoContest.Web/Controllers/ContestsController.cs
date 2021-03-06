﻿namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Common;
    using Common.Filters;
    using Common.Helpers;
    using Data.Contracts;
    using InputModels;
    using PagedList;
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
                .ProjectTo<ContestViewModel>()
                .ToList();

            return View(contests);
        }

        //GET: Contests/Details/5
        public ActionResult Details(int id)
        {
            var contest = this.Data.Contests
                .All()
                .Where(x => x.Id == id)
                .ProjectTo<ContestViewModel>()
                .FirstOrDefault();

            return this.View(contest);
        }

        public ActionResult ArchivedDetails(int id)
        {
            var contest = this.Data.Contests
                .AllDeleted()
                .Where(x => x.Id == id)
                .ProjectTo<ContestViewModel>()
                .FirstOrDefault();

            return this.View(contest);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ContestInputModel();
            return this.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ContestInputModel contest)
        {
            if (ModelState.IsValid &&
                contest.Title != null &&
                contest.Description != null)
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
            var oldContest = this.Data.Contests
                .All()
                .Where(c => c.Id == id)
                .ProjectTo<ContestViewModel>()
                .FirstOrDefault();

            if (oldContest == null)
            {
                return this.HttpNotFound();
            }

            if (oldContest.Owner.Id != this.UserProfile.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot edit a contest which is not yours.");
            }

            return View(oldContest);
        }

        public ActionResult Update(ContestViewModel model)
        {
            var contest = this.Data.Contests.GetById(model.Id);

            if (ModelState.IsValid)
            {
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

            return View("Edit", model);
        }

        public ActionResult InviteUsers(int id)
        {
            var users = this.Data.Users.All()
                .OrderByDescending(u => u.JoinedOn)
                .ThenBy(u => u.UserName)
                .ProjectTo<UserViewModel>();

            var contestInviteUsers = new ContestInviteUsers
            {
                Users = users,
                ContestId = id
            };

            return this.View("ContestInviteUsers", contestInviteUsers);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Invite(int id, string username)
        {
            var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);

            var contest = this.Data.Contests.GetById(id);

            if (contest == null)
            {
                return this.HttpNotFound();
            }

            if (user == this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot invite yourself. Be free to participate.");
            }

            if (contest.OwnerId != this.UserProfile.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot invite participants in a contest which is not yours.");
            }

            if (contest.Participants.Contains(user))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "User is already invited or has joined your contest.");
            }

            if (!contest.Participants.Contains(user))
            {
                contest.Participants.Add(user);
                this.Data.SaveChanges();
            }

            return new EmptyResult();
        }

        // GET: /Contests/Delete/5
        [ChildActionOnly]
        public ActionResult Delete(int contestId)
        {
            var contest = this.Data.Contests
                .All()
                .Where(c => c.Id == contestId)
                .ProjectTo<ContestViewModel>()
                .FirstOrDefault();

            if (contest == null)
            {
                return HttpNotFound();
            }

            if (contest.Owner != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot delete a contest which is not yours.");
            }

            return View(contest);
        }

        // POST: /Contests/Delete/5
        [HttpPost, ValidateAntiForgeryToken]
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
            var contest = this.Data.Contests
                .All()
                .Where(c => c.Id == id)
                .ProjectTo<ContestViewModel>()
                .FirstOrDefault();

            return PartialView("Delete", contest);

        }

        [HttpPost]
        public ActionResult Finalize(int id)
        {
            var contest = this.Data.Contests
                .All()
                .FirstOrDefault(c => c.Id == id);

            if (contest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Contest cannot be null.");
            }

            var winnerPicture = this.Data.Contests.GetById(id)
                .Pictures
                .OrderByDescending(p => p.Votes.Count)
                .ThenByDescending(p => p.CreatedOn)
                .AsQueryable()
                .ProjectTo<PictureViewModel>()
                .FirstOrDefault();

            if (winnerPicture == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Contest cannot be finalized without a winner.");
            }

            contest.IsClosedForSubmissions = true;
            contest.IsClosedForVoting = true;
            this.Data.Contests.Delete(contest);
            this.Data.SaveChanges();

            return new EmptyResult();
        }

        [HttpGet]
        public ActionResult Winner(int id)
        {
            var winnerPicture = this.Data.Contests.GetById(id)
                .Pictures
                .OrderByDescending(p => p.Votes.Count)
                .ThenByDescending(p => p.CreatedOn)
                .AsQueryable()
                .ProjectTo<PictureViewModel>()
                .FirstOrDefault();

            if (winnerPicture == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Contest cannot be finalized without a winner.");
            }

            winnerPicture.Url = Dropbox.Download(winnerPicture.Url);

            return View(winnerPicture);
        }

        [HttpGet, Authorize]
        [AjaxOnly]
        public ActionResult MyContests(int? page)
        {
            var userId = this.UserProfile.Id;

            var myContests = this.Data.Contests
                .All()
                .Where(c => c.OwnerId == userId)
                .OrderByDescending(c => c.CreatedOn)
                .ProjectTo<ContestViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.PartialView("Partial/_MyContests", myContests);
        }

        [HttpGet, AllowAnonymous]
        [AjaxOnly]
        public ActionResult ArchivedContests(int? page)
        {
            var archivedContests = this.Data.Contests
                .AllDeleted()
                .OrderByDescending(c => c.CreatedOn)
                .ProjectTo<ContestViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.PartialView("Partial/_ArchivedContests", archivedContests);
        }

        [HttpGet, AllowAnonymous]
        [AjaxOnly]
        public ActionResult ActiveContests(int? page)
        {
            var activeContests = this.Data.Contests
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .ProjectTo<ContestViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.PartialView("Partial/_ActiveContests", activeContests);
        }

        [HttpGet, AllowAnonymous]
        public ActionResult AllContests(int? page)
        {
            var allContests = this.Data.Contests
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .ProjectTo<ContestViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.PartialView("Partial/_AllContests", allContests);
        }
    }
}