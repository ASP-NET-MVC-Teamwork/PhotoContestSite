namespace PhotoContest.Web.Areas.Administartor.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Common;
    using Data.Contracts;
    using PagedList;
    using PhotoContest.Models;
    using ViewModels;
    using Web.Controllers;
    [Authorize]
    public class AdminUsersController : BaseController
    {
        public AdminUsersController(IPhotoContestData data) : base(data)
        {
        }
        // GET: Admin/Users
        public ActionResult Index(int? page)
        {
            var users = this.Data.Users.All()
                .OrderByDescending(u => u.JoinedOn)
                .ThenBy(u => u.UserName)
                .ProjectTo<UserViewModel>()
                .ToPagedList(page ?? 1, GlobalConstants.DefaultPageSize);

            return this.View(users);
        }

        [ChildActionOnly]
        public ActionResult Delete(string username)
        {
            var user = this.Data.Users
                .All()
                .Where(c => c.UserName == username)
                .ProjectTo<UserViewModel>()
                .FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();
            }


            return View(user);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(UserViewModel model)
        {
            var user = this.Data.Users.All().FirstOrDefault(c => c.UserName == model.UserName);

            if (user == null)
            {
                return HttpNotFound();
            }

            this.Data.Users.Delete(user);
            user.UserName = Guid.NewGuid().ToString();
            this.Data.SaveChanges();

            return RedirectToAction("Index", "AdminUsers");
        }

        [HttpGet]
        public PartialViewResult GetDeletePartial(string username)
        {
            var user = this.Data.Users
                .All()
                .Where(c => c.UserName == username)
                .ProjectTo<UserViewModel>()
                .FirstOrDefault();

            return PartialView("Delete", user);

        }
        public ActionResult Search(string query)
        {
            var results = this.Data.Users
                .All()
                .Where(u => u.UserName.ToLower().Contains(query))
                .ProjectTo<UserViewModel>();

            return this.PartialView("Partial/_UserResults", results);
        }

    }
}