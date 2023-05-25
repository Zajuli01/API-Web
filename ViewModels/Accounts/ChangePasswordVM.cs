namespace API_Web.ViewModels.Accounts;

public class ChangePasswordVM
{
    public string Email { get; set; }
    public int Otp { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
