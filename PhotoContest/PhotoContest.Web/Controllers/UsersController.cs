namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Data.Contracts;
    using PhotoContest.Models;
    using ViewModels;

    public class UsersController : BaseController
    {
        public UsersController(IPhotoContestData data) 
            : base(data)
        {
        }

        public UsersController(IPhotoContestData data, ApplicationUser userProfile) 
            : base(data, userProfile)
        {
        }


        public ActionResult Index()
        {
            var users = this.Data.Users.All()
                .OrderByDescending(u => u.JoinedOn)
                .ThenBy(u => u.UserName)
                .ProjectTo<UserViewModel>()
                .ToList();

            return this.View(users);
        }

        //TODO
        public ActionResult Details(string id)
        {
            var user = this.Data.Users
                .All()
                .Where(u => u.Id == id)
                .Project()
                .To<UserViewModel>()
                .FirstOrDefault();

            return this.View(user);
        }


    }
}