﻿namespace PhotoContest.Web.Controllers
{
    using System;
    using System.Linq;
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
            var contest = this.Data.Contests.GetById(id);

            return View(contest);
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
                    Author = this.UserProfile,
                    CreatedOn = DateTime.Now,
                    ContestId = id,
                    IsDeleted = false
                };

                this.Data.Pictures.Add(picture);

                this.Data.SaveChanges();
                return RedirectToAction("Details", new { pictureId = picture.PictureId });
            }
            return this.View(model);
        }

        //GET: Details/5
        public ActionResult Details(int id, int pictureId)
        {
            var picture = this.Data.Pictures
                .All()
                .Where(x => x.PictureId == pictureId && x.ContestId == id)
                .Project()
                .To<PictureViewModel>()
                .FirstOrDefault();

            return this.View(picture);
        }
    }
}