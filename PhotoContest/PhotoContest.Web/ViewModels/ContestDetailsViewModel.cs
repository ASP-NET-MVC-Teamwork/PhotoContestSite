namespace PhotoContest.Web.ViewModels
{
    using Common.Mappings;
    using PhotoContest.Models;
    using PhotoContest.Models.Enums;

    public class ContestDetailsViewModel : IMapFrom<Contest>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public virtual ApplicationUser Owner { get; set; }

        public RewardStrategy RewardStrategy { get; set; }

        public ContestType Type { get; set; }

        public VotingStrategy VotingStrategy { get; set; }

        public DeadlineStrategy DeadlineStrategy { get; set; }

        public bool IsClosedForSubmissions { get; set; }

        public bool IsClosedForVoting { get; set; }

    }
}