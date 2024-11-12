using API.Attributes;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO.Request
{
    public class LoginRequest
    {
        [PhoneValidation]
        public string Phone {  get; set; }

        [Required(ErrorMessage = "Поле не может быть пустым.")]
        [StringLength(20, ErrorMessage = "Пароль не может быть больше 20 символов.")]
        public string Password { get; set; }
    }
}
