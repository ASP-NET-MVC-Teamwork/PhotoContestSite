using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoContest.Web.Controllers
{
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using InputModels;
    using PhotoContest.Models;
    using ViewModels;

    [Authorize]
    public class PicturesController : BaseController
    {
        public PicturesController(IPhotoContestData data, ApplicationUser userProfile) : base(data, userProfile)
        {
        }
        // GET: Pictures
        public ActionResult  Index(int id)
        {
            var contest = this.Data.Contests.GetById(id);

            return View(contest);
            
        }

        public ActionResult Create()
        {
            var model = new PictureInputModel();
            return this.View(model);
        }

        // POST: Pictures
        [HttpPost]
        public ActionResult Create(PictureInputModel model,int id)
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
                   IsDeleted = false,
                   

               }; 

                this.Data.Pictures.Add(picture);
                
                this.Data.SaveChanges();
                return RedirectToAction("Details", new { id = picture.Id });
            }
            return this.View(model);
        }
        //GET: Details/5
        public ActionResult Details(int id)
        {
            var picture = this.Data.Pictures
                .All()
                .Where(x => x.Id == id)
                .Project()
                .To<PictureViewModel>()
                .FirstOrDefault();

            return this.View(picture);
        }


    }
}