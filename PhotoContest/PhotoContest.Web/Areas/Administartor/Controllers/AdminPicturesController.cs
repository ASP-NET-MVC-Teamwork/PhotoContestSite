namespace PhotoContest.Web.Areas.Administartor.Controllers
{
    using Data.Contracts;
    using Web.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Common.Helpers;
    using PhotoContest.Models;
    using ViewModels;

    public class AdminPicturesController : BaseController
    {
        public AdminPicturesController(IPhotoContestData data) : base(data)
        {
        }
        // GET: Administartor/AdminPictures
        public ActionResult Index(int id)
        {
            var pictures = this.Data.Pictures
                .All()
                .Where(p => p.ContestId == id && p.IsDeleted == false)
                .OrderByDescending(c => c.CreatedOn)
                .Project()
                .To<PictureViewModel>()
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

            return this.RedirectToAction("Index", new { id = picture.ContestId });
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

            this.Data.Pictures.Delete(picture);
            this.Data.SaveChanges();

            return RedirectToAction("Index", "AdminPictures", new { id = model.ContestId });
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