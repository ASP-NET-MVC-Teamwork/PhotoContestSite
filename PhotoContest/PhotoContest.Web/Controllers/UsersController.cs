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
                .ProjectTo<UserViewModel>();
            
            return this.View(users);
        }

        [Authorize]
        public ActionResult Details(string id)
        {
            var user = this.Data.Users
                .All()
                .Where(u => u.Id == id)
                .ProjectTo<UserViewModel>()
                .FirstOrDefault();

            return this.View(user);
        }

        public ActionResult Search(string query)
        {
            var results = this.Data.Users
                .All()
                .Where(u => u.UserName.ToLower().Contains(query))
                .ProjectTo<UserViewModel>();
            
            return this.PartialView("_UserResults", results);
        }
    }
}