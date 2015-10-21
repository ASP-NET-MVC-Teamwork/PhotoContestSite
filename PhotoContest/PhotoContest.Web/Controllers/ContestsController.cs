namespace PhotoContest.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Data.Contracts;
    using PhotoContest.Models;

    public class ContestsController : BaseController
    {
        public ContestsController(IPhotoContestData data)
            : base(data)
        {
        }



        // GET: Contests
        public ActionResult Index()
        {
            var contests = this.Data.Contests.All().Include(c => c.Owner);
            return View(contests.ToList());
        }

        // GET: Contests/Details/5
        public ActionResult Details(int id)
        {
            Contest contest = this.Data.Contests.GetById(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        // GET: Contests/Create
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(this.Data.Users.All(), "Id", "ProfilePictureUrl");
            return View();
        }

        // POST: Contests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,OwnerId,IsDeleted,DeletedOn,RewardStrategy,Type,VotingStrategy,DeadlineStrategy,IsClosedForSubmissions,IsClosedForVoting,CreatedOn,ModifiedOn")] Contest contest)
        {
            if (ModelState.IsValid)
            {
                this.Data.Contests.Add(contest);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(this.Data.Users.All(), "Id", "ProfilePictureUrl", contest.OwnerId);
            return View(contest);
        }

        // GET: Contests/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = this.Data.Contests.GetById(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(this.Data.Users.All(), "Id", "ProfilePictureUrl", contest.OwnerId);
            return View(contest);
        }

        // POST: Contests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,OwnerId,IsDeleted,DeletedOn,RewardStrategy,Type,VotingStrategy,DeadlineStrategy,IsClosedForSubmissions,IsClosedForVoting,CreatedOn,ModifiedOn")] Contest contest)
        {
            if (ModelState.IsValid)
            {
                this.Data.Contests.Update(contest);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(this.Data.Users.All(), "Id", "ProfilePictureUrl", contest.OwnerId);
            return View(contest);
        }

        // GET: Contests/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contest contest = this.Data.Contests.GetById(id);
            if (contest == null)
            {
                return HttpNotFound();
            }
            return View(contest);
        }

        // POST: Contests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contest contest = this.Data.Contests.GetById(id);
            this.Data.Contests.Delete(contest);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        this.Data.Contests.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
