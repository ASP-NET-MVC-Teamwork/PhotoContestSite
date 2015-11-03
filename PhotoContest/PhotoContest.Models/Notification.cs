namespace PhotoContest.Models
{
    using System;
    using Common;

    public class Notification : AuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public string SenderId { get; set; }
        
        public virtual ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual ApplicationUser Receiver { get; set; }

        public int ContestId { get; set; }

        public virtual Contest Contest { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
