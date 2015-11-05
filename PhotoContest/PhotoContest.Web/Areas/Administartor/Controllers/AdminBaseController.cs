namespace PhotoContest.Web.Areas.Administartor.Controllers
{
    using Data.Contracts;
    using PhotoContest.Models;
    using Web.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;


    [Authorize(Roles = "Admin")]
    public class AdminBaseController : BaseController
    {
        public AdminBaseController(IPhotoContestData data) : base(data)
        {
        }
    }
}