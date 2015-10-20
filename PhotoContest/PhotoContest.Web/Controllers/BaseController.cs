using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoContest.Web.Controllers
{
    using System.Web.Routing;
    using Data.UnitOfWork;
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

        protected IPhotoContestData Data { get; private set; }

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