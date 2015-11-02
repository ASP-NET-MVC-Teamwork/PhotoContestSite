namespace PhotoContest.Web.ViewModels
{
    using Common.Mappings;
    using PhotoContest.Models;
    using PhotoContest.Models.Enums;

    public class InvitationViewModel : IMapFrom<Invitation>
    {
        public int Id { get; set; }

        public UserViewModel Sender { get; set; }

        public UserViewModel Target { get; set; }

        public int ContestId { get; set; }

        public ContestViewModel Contest { get; set; }

        public InvitationStatus InvitationStatus { get; set; }
    }
}