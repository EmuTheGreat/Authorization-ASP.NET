using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace API.Attributes
{
    public class PhoneValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return new ValidationResult("Номер телефона не может быть пустым.");
            }
            if (phoneNumber.Length == 11 &&
                phoneNumber.StartsWith("7") &&
                Regex.IsMatch(phoneNumber, @"^\d{11}$"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage ?? "Неверный формат номера телефона. Телефон должен начинаться с 7 и иметь 11 символов.");
            }
        }

    }
}
