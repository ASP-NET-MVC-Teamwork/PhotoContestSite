namespace PhotoContest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Enums;
    using PhotoContest.Common;

    public class Contest : AuditInfo, IDeletableEntity
    {
        private ICollection<Picture> pictures;
        private ICollection<ApplicationUser> participants;
        private ICollection<Reward> rewards;

        public Contest()
        {
            this.CreatedOn = DateTime.Now;
            this.pictures = new HashSet<Picture>();
            this.participants = new HashSet<ApplicationUser>();
            this.rewards = new HashSet<Reward>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GlobalConstants.TitleMinLength)]
        [MaxLength(GlobalConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MinLength(GlobalConstants.DescriptionMinLength)]
        [MaxLength(GlobalConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public ContestType Type { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public DeadlineStrategy DeadlineStrategy { get; set; }
        
        public bool IsClosedForSubmissions { get; set; }

        public bool IsClosedForVoting { get; set; }

        public virtual ICollection<Picture> Pictures
        {
            get { return this.pictures; }
            set { this.pictures = value; }
        }

        public virtual ICollection<ApplicationUser> Participants
        {
            get { return this.participants; }
            set { this.participants = value; }
        }

        public virtual ICollection<Reward> Reward
        {
            get { return this.rewards; }
            set { this.rewards = value; }
        }
    }
}
