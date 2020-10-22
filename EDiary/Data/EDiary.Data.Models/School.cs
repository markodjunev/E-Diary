namespace EDiary.Data.Models
{
    using System.Collections.Generic;

    using EDiary.Data.Common.Models;

    public class School : BaseDeletableModel<int>
    {
        public School()
        {
            this.Users = new HashSet<ApplicationUser>();
        }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
