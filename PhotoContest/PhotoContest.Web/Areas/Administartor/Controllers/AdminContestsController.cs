namespace PhotoContest.Web.Areas.Administartor.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Common;
    using Data.Contracts;
    using PagedList;
    using PhotoContest.Models;
    using ViewModels;
    using Web.Controllers;

    public class AdminContestsController : AdminBaseController
    {
        public AdminContestsController(IPhotoContestData data) : base(data)
        {
        }

        // GET: Administartor/AdminContests
        public ActionResult Index(int? page)
        {
            var contests = this.Data.Contests
                .All()
                .OrderByDescending(c => c.CreatedOn)
                .ProjectTo<ContestViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return View(contests);
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

            return this.RedirectToAction("Index");
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

            return RedirectToAction("Index", "AdminContests", new { id = model.Id });
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


    }
}