using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class EmailValidationAttribute : ValidationAttribute
{
    private readonly int _maxLength;
    public EmailValidationAttribute(int maxLength)
    {
        _maxLength = maxLength;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var email = value as string;
        
        if (email?.Length > _maxLength)
        {
            return new ValidationResult($"Электронная почта не может превышать {_maxLength} символов.");
        }

        if (string.IsNullOrEmpty(email))
        {
            return new ValidationResult("Электронная почта не может быть пустой.");
        }
        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (emailRegex.IsMatch(email))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult(ErrorMessage ?? "Неверный формат электронной почты.");
        }
    }
}
