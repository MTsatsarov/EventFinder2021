// ReSharper disable VirtualMemberCallInConstructor
namespace EventFinder2021.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using EventFinder2021.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Events = new HashSet<Event>();
            this.GoingUsers = new HashSet<GoingUsers>();
            this.NotGoingUsers = new HashSet<NotGoingUsers>();
            this.Likes = new HashSet<Like>();
            this.Dislikes = new HashSet<Dislike>();
            this.Votes = new HashSet<Vote>();
        }

        public virtual ICollection<GoingUsers> GoingUsers { get; set; }

        public virtual ICollection<NotGoingUsers> NotGoingUsers { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Comentary> Comentaries { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Event> Events { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Reply> Replies { get; set; }

        [InverseProperty("Users")]
        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Dislike> Dislikes { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
