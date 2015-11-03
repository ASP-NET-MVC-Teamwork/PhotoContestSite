namespace PhotoContest.Web.ViewModels
{
    using Common.Mappings;
    using PhotoContest.Models;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public bool IsSeen { get; set; }
    }
}