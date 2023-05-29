using API_Web.Utility;
using System.ComponentModel.DataAnnotations;

namespace API_Web.ViewModels.Accounts;

public class RegisterVM
{
    //[EmailPhoneValidation(nameof(NIK))]
    //public string? NIK { get; set; }

    [Required(ErrorMessage ="FirstName is required")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }

    public DateTime HiringDate { get; set; }
    [EmailAddress]
    [EmailPhoneValidation(nameof(Email))]
    public string Email { get; set; }

    [Phone]
    [EmailPhoneValidation("PhoneNumber")]
    public string PhoneNumber { get; set; }

    public string Major { get; set; }

    public string Degree { get; set; }

    [Range(0,4, ErrorMessage ="Must between 0 and 4")]
    public float GPA { get; set; }

    //public Guid UniversityGuid { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    [PasswordValidation(ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special symbol, and have a minimum length of 6 characters.")]
    public string Password { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    // public University? University { get; set; }
}
