namespace PhotoContest.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Mappings;
    using PhotoContest.Models;
    using PhotoContest.Models.Enums;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public ContestType Type { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public DeadlineStrategy DeadlineStrategy { get; set; }
        [Display(Name = "Closed for submissions")]
        public bool IsClosedForSubmissions { get; set; }
        [Display(Name = "Closed for voting")]
        public bool IsClosedForVoting { get; set; }
    }
}