using System.ComponentModel.DataAnnotations;

namespace ItProger.Models.Category
{
    public class Phone 
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Додайте вартість")]
        [Required(ErrorMessage = "Вам потрібно ввести вартість")]
        public int Price { get; set; }

        [Display(Name = "Тип екрану")]
        public string ScreenType { get; set; }

        [Display(Name = "Додайте модель")]
        [Required(ErrorMessage = "Вам потрібно ввести модель")]
        public string Title { get; set; }

        [Display(Name = "Додайте опис")]
        public string? Description { get; set; }

        public string SrcImage { get; set; }
    }
}
