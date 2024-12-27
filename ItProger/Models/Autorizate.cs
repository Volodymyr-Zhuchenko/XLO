using System.ComponentModel.DataAnnotations;

namespace ItProger.Models
{
    public class Autorizate
    {

        [Required(ErrorMessage = "Вам потрібно ввести Email")]
        [Display(Name = "Введіть Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вам потрібно ввести пароль")]
        [Display(Name = "Введіть пароль")]
        public string Password { get; set; }
    }
}
