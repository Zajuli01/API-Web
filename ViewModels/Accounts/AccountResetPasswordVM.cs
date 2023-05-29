namespace API_Web.ViewModels.Accounts;

using System.ComponentModel.DataAnnotations;

public class AccountResetPasswordVM
{
    [Required(ErrorMessage = "OTP is required")]
    [Display(Name = "OTP")]
    public int OTP { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    [Display(Name = "Email")]
    public string Email { get; set; }
}
