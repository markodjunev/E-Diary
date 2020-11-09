// ReSharper disable VirtualMemberCallInConstructor
namespace EDiary.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EDiary.Data.Common.Models;
    using EDiary.Data.Models.Enums;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public string UniqueCitizenshipNumber { get; set; }

        public string ParentUserId { get; set; }

        public virtual ApplicationUser ParentUser { get; set; }

        public int? SchoolId { get; set; }

        public virtual School School { get; set; }

        public Class? Class { get; set; }

        public TypeOfClass? TypeOfClass { get; set; }

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
