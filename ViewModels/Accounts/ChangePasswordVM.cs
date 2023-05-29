using API_Web.Utility;
using System.ComponentModel.DataAnnotations;

namespace API_Web.ViewModels.Accounts;

public class ChangePasswordVM
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "OTP is required")]
    [Display(Name = "OTP")]
    public int Otp { get; set; }

    [PasswordValidation(ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special symbol, and have a minimum length of 6 characters.")]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; }

    [PasswordValidation(ErrorMessage = "Password does not match")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; }
}