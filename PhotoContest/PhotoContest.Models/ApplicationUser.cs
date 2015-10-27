namespace PhotoContest.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser
    {
        private ICollection<Picture> uploadedPictures;
        private ICollection<Contest> contests;
        private ICollection<Comment> comments;
        private ICollection<Vote> votes;

        public ApplicationUser()
        {
            this.uploadedPictures = new HashSet<Picture>();
            this.contests = new HashSet<Contest>();
            this.comments = new HashSet<Comment>();
            this.votes = new HashSet<Vote>();
        }

        public DateTime JoinedOn { get; set; }

        public virtual ICollection<Picture> UploadedPictures
        {
            get { return this.uploadedPictures; }
            set { this.uploadedPictures = value; }
        }

        public virtual ICollection<Contest> Contests
        {
            get { return this.contests; }
            set { this.contests = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public byte[] ProfilePicture { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
