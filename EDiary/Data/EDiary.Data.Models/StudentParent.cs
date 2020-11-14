namespace EDiary.Data.Models
{
    using EDiary.Data.Common.Models;

    public class StudentParent : BaseDeletableModel<int>
    {
        public string StudentId { get; set; }

        public virtual ApplicationUser Student { get; set; }

        public string ParentId { get; set; }

        public virtual ApplicationUser Parent { get; set; }
    }
}
