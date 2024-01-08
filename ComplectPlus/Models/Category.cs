using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ComplectPlus.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Укажите название")]
        [Display(Name = "Название")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string? CategoryName { get; set; }//название категории

        public string? CategoryN
        {
            get
            {
                return CategoryName;
            }
        }
       // public virtual ICollection<Component>? Component { get; set; }
    }
}
