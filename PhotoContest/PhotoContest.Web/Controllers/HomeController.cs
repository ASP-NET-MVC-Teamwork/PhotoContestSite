using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoContest.Web.Controllers
{
    using Data.UnitOfWork;
    using PhotoContest.Models;

    public class HomeController : BaseController
    {
        public HomeController(IPhotoContestData data) : base(data)
        {
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}