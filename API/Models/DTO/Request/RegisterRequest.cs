using System.ComponentModel.DataAnnotations;
using API.Attributes;

namespace API.Models.DTO.Request
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Укажите ФИО.")]
        [StringLength(250)]
        public string FIO { get; set; }

        [PhoneValidation]
        public string Phone {  get; set; }

        [EmailValidation(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым.")]
        [StringLength(20, ErrorMessage = "Пароль не может быть больше 20 символов.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым.")]
        [StringLength(20, ErrorMessage = "Пароль не может быть больше 20 символов.")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

    }
}
