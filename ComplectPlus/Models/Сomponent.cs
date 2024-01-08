using System.ComponentModel.DataAnnotations;

namespace ComplectPlus.Models
{
    public class Сomponent
    {
        [Key]
        public int ComponentId { get; set; }

        [Required(ErrorMessage = "Укажите название")]
        [Display(Name = "Название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string? ComponentsName { get; set; }//назввание

        [Display(Name = "Год релиза")]
        public int YearRelease { get; set; }//год релиза

        [Display(Name = "Категория")]
        public int CategoryId { get; set; }//категория

        [Display(Name = "Производитель")]
        public int ManufacturerId { get; set; }//производитель

        [Display(Name = "Описание")]
        public string? Description { get; set; }//описание
        [Display(Name = "Категория")]
        public virtual Category? Categories { get; set; }
        [Display(Name = "Производитель")]
        public virtual Manufacturer? Manufacturers { get; set; }
      
    }
}
