namespace EDiary.Web.ViewModels.Administration.Schools.OutputViewModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class SchoolsViewModel : IMapFrom<School>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string ImageUrl { get; set; }
    }
}
