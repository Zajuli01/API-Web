using API_Web.Model;
using API_Web.ViewModels.Universities;

namespace API_Web.ViewModels.Educations;

public class EducationVM
{
    public Guid? Guid { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public float GPA { get; set; }
    public Guid UniversityGuid { get; set; }

    public static EducationVM ToVM(Education education)
    {
        return new EducationVM
        {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree
        };
    }
    //public static Education ToModel(EducationVM educationVM)
    //{
    //    return new Education()
    //    {
    //        Guid = System.Guid.NewGuid(),
    //        Major = educationVM.Major,
    //        Degree = educationVM.Degree,
    //        GPA = educationVM.GPA,
    //        UniversityGuid = educationVM.UniversityGuid,
    //        CreatedDate = DateTime.Now,
    //        ModifiedDate = DateTime.Now
    //    };
    //}
}
