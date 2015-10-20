namespace PhotoContest.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Data.Contracts;
    using PhotoContest.Models;

    public class BaseController : Controller
    {
        private IPhotoContestData data;
        private ApplicationUser userProfile;

        protected BaseController(IPhotoContestData data)
        {
            this.Data = data;
        }

        protected BaseController(IPhotoContestData data, ApplicationUser userProfile)
            : this(data)
        {
            this.UserProfile = userProfile;
        }

        protected IPhotoContestData Data { get; }

        protected ApplicationUser UserProfile { get; private set; }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = requestContext.HttpContext.User.Identity.Name;
                var user = this.Data.Users.All().FirstOrDefault(u => u.UserName == username);
                this.UserProfile = user;
            }

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}