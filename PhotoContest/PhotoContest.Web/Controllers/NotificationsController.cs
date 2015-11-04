namespace PhotoContest.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using Common.Filters;
    using Data.Contracts;
    using InputModels;
    using PhotoContest.Models;
    using ViewModels;

    public class NotificationsController : BaseController
    {
        public NotificationsController(IPhotoContestData data) : base(data)
        {
        }

        // GET: Notifications
        [Authorize]
        [AjaxOnly]
        public ActionResult Index()
        {
            var notifications = this.Data.Notifications
                .All()
                .Where(n => n.IsDeleted == false && n.ReceiverId == this.UserProfile.Id)
                .OrderByDescending(n => n.CreatedOn)
                .ProjectTo<NotificationViewModel>();

            return this.PartialView(notifications);
        }

        [Authorize]
        [AjaxOnly]
        public JsonResult GetNotificationsCount()
        {
            var notifications = this.Data.Notifications
                .All()
                .Count(n => n.ReceiverId == this.UserProfile.Id && n.IsDeleted == false);


            return Json(notifications, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult Create()
        //{
        //    var model = new NotificationInputModel();
        //    return this.View(model);
        //}

        [HttpPost]
        [AjaxOnly]
        public ActionResult Create(NotificationInputModel notification)
        {
            if (ModelState.IsValid)
            {
                var newNotification = new Notification()
                {
                    SenderId = this.UserProfile.Id,
                    ReceiverId = notification.RecieverId,
                    ContestId = notification.ContestId,
                };

                this.Data.Notifications.Add(newNotification);

                this.Data.SaveChanges();             
            }
            return new EmptyResult();
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Update(int notificationId)
        {
            var notification = this.Data.Notifications.GetById(notificationId);

            if (notification == null)
            {
                return this.HttpNotFound();
            }

            if (notification.Receiver.Id != this.UserProfile.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot edit a contest which is not yours.");
            }
            
            this.Data.Notifications.Delete(notification);
            
            this.Data.SaveChanges();

            return new EmptyResult();
        }
    }
}