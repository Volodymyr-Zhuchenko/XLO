using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ItProger.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Display(Name = "Введіть ім'я")]
        [Required(ErrorMessage ="Вам потрібно ввести ім'я")]
        public string Name {  get; set; }

        [Required(ErrorMessage = "Вам потрібно ввести прізвище")]
        [Display(Name = "Введіть прізвище")]
        public string SurName { get; set; }

        [Required(ErrorMessage = "Вам потрібно ввести вік")]
        [Range(1, 120, ErrorMessage = "Вік має бути в межах від 1 до 120 років")]
        [Display(Name = "Введіть вік")]
        public int? Age { get; set; }

        [Required(ErrorMessage = "Вам потрібно ввести Email")]
        [Display(Name = "Введіть Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Вам потрібно ввести пароль")]
        [Display(Name = "Введіть пароль")]
        public string Password { get; set; }
    }
}
