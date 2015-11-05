namespace PhotoContest.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Common.Helpers;
    using Data.Contracts;
    using InputModels;
    using PhotoContest.Models;
    using PhotoContest.Models.Enums;
    using ViewModels;

    [Authorize]
    public class PicturesController : BaseController
    {
        public PicturesController(IPhotoContestData data)
            : base(data)
        {
        }

        // GET: Pictures
        public ActionResult Index(int id)
        {
            var pictures = this.Data.Pictures
                .All()
                .Where(p => p.ContestId == id && p.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .ProjectTo<PictureViewModel>()
                .ToList();

            foreach (var picture in pictures)
            {
                picture.Url = Dropbox.Download(picture.Url);
            }

            var picturesViewModel = new ContestPicturesViewModel
            {
                Pictures = pictures,
                ContestId = id
            };

            return View(picturesViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new PictureInputModel();
            return this.View(model);
        }

        // POST: Pictures
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PictureInputModel model, int id)
        {
            var contest = this.Data.Contests.GetById(id);

            if (contest.Type == ContestType.Private && !contest.Participants.Contains(this.UserProfile) &&
                contest.Type == ContestType.Private && contest.Owner != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The contest is private. You need to be invited by the owner.");
            }

            if (contest.IsDeleted || contest.IsClosedForSubmissions)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The contest is closed for submissions.");
            }

            string path = Dropbox.Upload(model.Photo.FileName, this.UserProfile.UserName, model.Photo.InputStream);

            var picture = new Picture()
            {
                Title = model.Title,
                Url = path,
                Owner = this.UserProfile,
                CreatedOn = DateTime.Now,
                ContestId = id,
                IsDeleted = false
            };

            this.Data.Pictures.Add(picture);

            if (!contest.Participants.Contains(this.UserProfile))
            {
                contest.Participants.Add(this.UserProfile);
            }

            this.Data.SaveChanges();

            return RedirectToAction("Index", new { id = picture.ContestId });
        }

        //GET: Details/5
        public ActionResult Details(int id, int pictureId)
        {
            var picture = this.Data.Pictures
                .All()
                .Where(x => x.PictureId == pictureId && x.ContestId == id)
                .ProjectTo<PictureViewModel>()
                .FirstOrDefault();

            if (picture == null)
            {
                return this.HttpNotFound();
            }

            picture.Url = Dropbox.Download(picture.Url);

            return this.View(picture);
        }

        [HttpPost]
        public ActionResult Vote(int id)
        {
            var picture = this.Data.Pictures.GetById(id);

            if (picture == null)
            {
                return this.HttpNotFound();
            }

            if (picture.Contest.IsDeleted || picture.Contest.IsClosedForVoting)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The contest is closed for voting.");
            }

            if (picture.Contest.Type == ContestType.Private && !picture.Contest.Participants.Contains(this.UserProfile) &&
                picture.Contest.Type == ContestType.Private && picture.Contest.Owner != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The contest is private for voting. You need to be invited by the owner to vote.");
            }

            if (picture.Votes.FirstOrDefault(v => v.User == this.UserProfile && v.IsDeleted == false) != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You have already voted!");
            }
            var vote = new Vote
            {
                Picture = picture,
                User = this.UserProfile
            };

            this.Data.Votes.Add(vote);
            this.Data.SaveChanges();

            var likeModel = new LikeViewModel
            {
                Likes = picture.Votes.Count(v => v.IsDeleted == false),
                PictureId = picture.PictureId,
                Action = "UnVote",
                ThumbDirection = "down"
            };

            return PartialView("_LikesCount", likeModel);
        }

        public ActionResult UnVote(int id)
        {
            var picture = this.Data.Pictures.GetById(id);

            if (picture == null)
            {
                return this.HttpNotFound();
            }

            if (picture.Contest.IsDeleted || picture.Contest.IsClosedForVoting)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The contest is closed for voting.");
            }

            if (picture.Votes.FirstOrDefault(v => v.User == this.UserProfile && v.DeletedOn == null && v.IsDeleted) != null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You have already unvoted!");
            }

            var vote = picture.Votes.FirstOrDefault(v => v.UserId == this.UserProfile.Id && v.IsDeleted == false);

            if (vote == null)
            {
                return this.HttpNotFound();
            }

            this.Data.Votes.Delete(vote);
            this.Data.SaveChanges();

            var likeModel = new LikeViewModel
            {
                Likes = picture.Votes.Count(v => v.IsDeleted == false),
                PictureId = picture.PictureId,
                Action = "Vote",
                ThumbDirection = "up"
            };

            return PartialView("_LikesCount", likeModel);
        }

        public ActionResult Edit(int id)
        {
            var oldPicture = this.Data.Pictures
                .All()
                .Where(p => p.PictureId == id)
                .ProjectTo<PictureViewModel>()
                .FirstOrDefault();

            if (oldPicture == null)
            {
                return this.HttpNotFound();
            }

            if (oldPicture.Owner.Id != this.UserProfile.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot edit a picture which is not yours.");
            }

            return View(oldPicture);
        }

        public ActionResult Update(PictureViewModel model)
        {
            var picture = this.Data.Pictures.GetById(model.PictureId);

            if (picture == null)
            {
                return this.HttpNotFound();
            }

            if (picture.OwnerId != this.UserProfile.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot edit a picture which is not yours.");
            }

            picture.Title = model.Title;
            picture.Url = model.Url;

            this.Data.SaveChanges();

            return this.RedirectToAction("Details", new { id = picture.ContestId, pictureId = model.PictureId });
        }

        // GET: /Pictures/Delete/5
        [ChildActionOnly]
        public ActionResult Delete(int pictureId)
        {
            Picture picture = this.Data.Pictures.GetById(pictureId);

            if (picture == null)
            {
                return HttpNotFound();
            }

            if (picture.Contest.IsDeleted)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The Contest is over you cannot delete pictures.");
            }

            if (picture.Owner != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot delete a picture which is not yours.");
            }
            
            return View(new PictureViewModel { PictureId = picture.PictureId, Title = picture.Title, Url = picture.Url, ContestId = picture.ContestId });
        }

        // POST: /Pictures/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PictureViewModel model)
        {
            Picture picture = this.Data.Pictures.GetById(model.PictureId);

            if (picture == null)
            {
                return HttpNotFound();
            }

            if (picture.Contest.IsDeleted)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "The Contest is over you cannot delete pictures.");
            }

            if (picture.Owner != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot delete a picture which is not yours.");
            }
            
            this.Data.Pictures.Delete(picture);
            this.Data.SaveChanges();

            return RedirectToAction("Index", "Pictures", new { id = model.ContestId });
        }

        [HttpGet]
        public PartialViewResult GetDeletePartial(int id)
        {
            var picture = this.Data.Pictures
                .All()
                .Where(p => p.PictureId == id)
                .ProjectTo<PictureViewModel>()
                .FirstOrDefault();

            return PartialView("Delete", picture);
        }
    }
}