namespace EDiary.Web.ViewModels.Administration.Roles.InputModels
{
    using EDiary.Data.Models;
    using EDiary.Services.Mapping;

    public class ApplicationRoleDropDownViewModel : IMapFrom<ApplicationRole>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
