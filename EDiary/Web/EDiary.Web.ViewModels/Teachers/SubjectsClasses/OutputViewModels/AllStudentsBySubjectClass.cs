namespace EDiary.Web.ViewModels.Teachers.SubjectsClasses.OutputViewModels
{
    using System.Collections.Generic;

    public class AllStudentsBySubjectClass
    {
        public IEnumerable<StudentsBySubjectClass> Students { get; set; }

        public SubjectClassInfoStudents SubjectClassInfo { get; set; }
    }
}
