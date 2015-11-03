namespace PhotoContest.Web.ViewModels
{
    using System.Collections.Generic;

    public class ContestInviteUsers
    {
        public IEnumerable<UserViewModel> Users { get; set; }

        public int ContestId { get; set; }
    }
}