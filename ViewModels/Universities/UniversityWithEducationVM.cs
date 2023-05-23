using API_Web.Model;
using API_Web.ViewModels.Educations;

namespace API_Web.ViewModels.Universities;

public class UniversityWithEducationVM
{
    public Guid? Guid { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public IEnumerable<EducationVM> Educations { get; set; }
}
