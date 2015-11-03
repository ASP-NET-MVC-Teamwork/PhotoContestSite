namespace PhotoContest.Web.InputModels
{
    using PhotoContest.Models;

    public class NotificationInputModel
    {
        public ApplicationUser Sender { get; set; }

        public ApplicationUser Receiver { get; set; }

        public Contest Contest { get; set; }
    }
}