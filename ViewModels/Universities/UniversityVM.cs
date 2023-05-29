using API_Web.Model;
using System.ComponentModel.DataAnnotations;

namespace API_Web.ViewModels.Universities;

public class UniversityVM
{
    [Display(Name = "Guid")]
    public Guid? Guid { get; set; }

    [Required(ErrorMessage = "Code is required")]
    [Display(Name = "Code")]
    public string Code { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Name")]
    public string Name { get; set; }

    //public static UniversityVM ToVM(University university)
    //{
    //    return new UniversityVM
    //    {
    //        Guid = university.Guid,
    //        Code = university.Code,
    //        Name = university.Name
    //    };
    //}

    //public static University ToModel(UniversityVM universityVM)
    //{
    //    return new University()
    //    {
    //        Guid = System.Guid.NewGuid(),
    //        Code = universityVM.Code,
    //        Name = universityVM.Name,
    //        CreatedDate = DateTime.Now,
    //        ModifiedDate = DateTime.Now
    //    };
    //}
}
