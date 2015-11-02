namespace PhotoContest.Models
{
    using Enums;

    public class Invitation
    {
        public Invitation()
        {
            this.InvitationStatus = InvitationStatus.Pending;
        }

        public int Id { get; set; }

        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public string TargetId { get; set; }

        public virtual ApplicationUser Target { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public InvitationStatus InvitationStatus { get; set; }
    }
}
