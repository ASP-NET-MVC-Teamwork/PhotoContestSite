using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoContest.Web.Controllers
{
    using System.Data.Entity;
    using Data.Contracts;

    public class ContestController : BaseController
    {
        public ContestController(IPhotoContestData data) : base(data)
        {
        }
        // GET: Contest
        public ActionResult Index()
        {
            var contests = this.Data.Contests.All().Include(c => c.Owner);
            return View();
        }

        
    }
}