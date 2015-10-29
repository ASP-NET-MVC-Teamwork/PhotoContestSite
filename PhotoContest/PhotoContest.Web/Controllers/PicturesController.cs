namespace PhotoContest.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using InputModels;
    using PhotoContest.Models;
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
            var picturesViewModel = new ContestPicturesViewModel
            {
                Pictures = this.Data.Pictures
                .All()
                .Where(p => p.ContestId == id && p.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<PictureViewModel>(),

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
            if (ModelState.IsValid)
            {
                var picture = new Picture()
                {
                    Title = model.Title,
                    Url = model.Url,
                    Owner = this.UserProfile,
                    CreatedOn = DateTime.Now,
                    ContestId = id,
                    IsDeleted = false
                };

                this.Data.Pictures.Add(picture);

                this.Data.SaveChanges();
                return RedirectToAction("Index", new { id = picture.ContestId });
            }
            return this.View(model);
        }

        //GET: Details/5
        public ActionResult Details(int id, int pictureId)
        {
            var picture = this.Data.Pictures
                .All()
                .Where(x => x.PictureId == pictureId && x.ContestId == id && x.IsDeleted == false)
                .Project()
                .To<PictureViewModel>()
                .FirstOrDefault();

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

            return PartialView("_LikesCount", picture.Votes.Count(v => v.IsDeleted == false));

        }

        public ActionResult UnVote(int id)
        {
            var picture = this.Data.Pictures.GetById(id);

            if (picture == null)
            {
                return this.HttpNotFound();
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

            vote.IsDeleted = true;
            vote.DeletedOn = DateTime.Now;
            this.Data.SaveChanges();

            return PartialView("_LikesCount", picture.Votes.Count(v => v.IsDeleted == false));
        }

        public ActionResult Edit(int id)
        {
            var oldPicture = this.Data.Pictures.GetById(id);
            if (oldPicture == null)
            {
                return this.HttpNotFound();
            }
            if (oldPicture.OwnerId != this.UserProfile.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot edit a picture which is not yours.");
            }
            return View(new PictureViewModel()
            {
                PictureId = oldPicture.PictureId,
                Title = oldPicture.Title,
                Url = oldPicture.Url
            });
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

            if (picture.Owner != this.UserProfile)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot delete a picture which is not yours.");
            }

            picture.IsDeleted = true;

            this.Data.SaveChanges();

            return RedirectToAction("Index", "Pictures", new { id = model.ContestId });
        }

        [HttpGet]
        public PartialViewResult GetDeletePartial(int id)
        {
            Picture picture = this.Data.Pictures.GetById(id);

            return PartialView("Delete", new PictureViewModel
            {
                PictureId = picture.PictureId,
                Title = picture.Title,
                Url = picture.Url,
                ContestId = picture.ContestId
            });
        }
    }
}