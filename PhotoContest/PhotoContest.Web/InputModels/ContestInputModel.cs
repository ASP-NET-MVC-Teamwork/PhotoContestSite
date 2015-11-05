namespace PhotoContest.Web.InputModels
{
    using System.ComponentModel.DataAnnotations;
    using Common;
    using PhotoContest.Models.Enums;

    public class ContestInputModel
    {
        [Required(ErrorMessage = "Title cannot be empty!")]
        [MaxLength(GlobalConstants.TitleMaxLength)]
        [MinLength(GlobalConstants.TitleMinLength)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description cannot be empty!")]
        [MaxLength(GlobalConstants.DescriptionMaxLength)]
        [MinLength(GlobalConstants.DescriptionMinLength)]
        public string Description { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public ContestType Type { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public DeadlineStrategy DeadlineStrategy { get; set; }

        public bool IsClosedForSubmissions { get; set; }

        public bool IsClosedForVoting { get; set; }
    }
}