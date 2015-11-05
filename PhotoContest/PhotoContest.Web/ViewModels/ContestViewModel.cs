namespace PhotoContest.Web.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common;
    using Common.Mappings;
    using PhotoContest.Models;
    using PhotoContest.Models.Enums;

    public class ContestViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title cannot be empty!")]
        [MinLength(GlobalConstants.TitleMinLength)]
        [MaxLength(GlobalConstants.TitleMaxLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description cannot be empty!")]
        [MinLength(GlobalConstants.DescriptionMinLength)]
        [MaxLength(GlobalConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        [Display(Name = "Reward Strategy")]
        public RewardStrategy RewardStrategy { get; set; }

        public ContestType Type { get; set; }

        [Display(Name = "Voting Strategy")]
        public VotingStrategy VotingStrategy { get; set; }

        [Display(Name = "Deadline Strategy")]    
        public DeadlineStrategy DeadlineStrategy { get; set; }

        [Display(Name = "Closed for submissions")]
        public bool IsClosedForSubmissions { get; set; }

        [Display(Name = "Closed for voting")]
        public bool IsClosedForVoting { get; set; }
    }
}