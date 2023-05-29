using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace API_Web.Utility;

using System.ComponentModel.DataAnnotations;

public class PasswordValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        var password = (string)value;
        if (password == null)
            return new ValidationResult("Password cannot be null.");

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
        var hasMinimum6Chars = new Regex(@".{6,}");

        var isValidated = hasNumber.IsMatch(password)
            && hasUpperChar.IsMatch(password)
            && hasLowerChar.IsMatch(password)
            && hasSymbols.IsMatch(password)
            && hasMinimum6Chars.IsMatch(password);

        if (!isValidated)
            return new ValidationResult("Password must contain at least one uppercase letter, one lowercase letter, one digit, one special symbol, and have a minimum length of 6 characters.");

        return ValidationResult.Success;
    }
}

