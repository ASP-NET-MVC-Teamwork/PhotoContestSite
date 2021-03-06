﻿namespace PhotoContest.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser, IDeletableEntity
    {
        private ICollection<Picture> uploadedPictures;
        private ICollection<Contest> contests;
        private ICollection<Comment> comments;
        private ICollection<Vote> votes;
        private ICollection<Notification> receivedNotifications;
        private ICollection<Notification> sentNotifications;

        public ApplicationUser()
        {
            this.uploadedPictures = new HashSet<Picture>();
            this.contests = new HashSet<Contest>();
            this.comments = new HashSet<Comment>();
            this.votes = new HashSet<Vote>();
            this.receivedNotifications = new HashSet<Notification>();
            this.sentNotifications = new HashSet<Notification>();
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

        [InverseProperty("Receiver")]
        public virtual ICollection<Notification> ReceivedNotifications
        {
            get { return this.receivedNotifications; }
            set { this.receivedNotifications = value; }
        }

        [InverseProperty("Sender")]
        public virtual ICollection<Notification> SentNotifications
        {
            get { return this.sentNotifications; }
            set { this.sentNotifications = value; }
        }

        public byte[] ProfilePicture { get; set; }
        
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
