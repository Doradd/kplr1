using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ComplectPlus.Models
{//производитель
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }

        [Required(ErrorMessage = "Укажите название")]
        [Display(Name = "Название производителя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string? ManufacturerName { get; set; }//название

        [Display(Name = "Полное название")]
        public string? ManufacturerPName { get; set; } //полное название
       // public virtual ICollection<Component>? Component { get; set; }
    }
}
