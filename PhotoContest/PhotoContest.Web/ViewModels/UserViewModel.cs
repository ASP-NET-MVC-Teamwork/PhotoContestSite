namespace PhotoContest.Web.ViewModels
{
    using System;
    using Common.Mappings;
    using PhotoContest.Models;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public DateTime JoinedOn { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}