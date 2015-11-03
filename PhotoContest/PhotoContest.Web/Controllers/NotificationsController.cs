namespace PhotoContest.Web.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
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
        public ActionResult Index()
        {
            var notifications = this.Data.Notifications
                .All()
                .Where(n => n.IsSeen == false)
                .ProjectTo<NotificationViewModel>()
                .ToList();

            return this.View(notifications);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new NotificationInputModel();
            return this.View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(NotificationInputModel notification)
        {
            if (ModelState.IsValid)
            {
                var newNotification = new Notification()
                {
                    Sender = this.UserProfile,
                    Contest = notification.Contest,
                    Receiver = notification.Receiver
                };

                this.Data.Notifications.Add(newNotification);

                this.Data.SaveChanges();
                return new EmptyResult();
            }
            return this.View(notification);
        }

        public ActionResult Update(NotificationViewModel model)
        {
            var notification = this.Data.Notifications.GetById(model.Id);

            if (notification == null)
            {
                return this.HttpNotFound();
            }

            if (notification.ReceiverId != this.UserProfile.Id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "You cannot edit a contest which is not yours.");
            }

            notification.Receiver = model.Receiver;
            notification.Sender = model.Sender;
            notification.Contest = model.Contest;

            this.Data.SaveChanges();

            return new EmptyResult();
        }
    }
}