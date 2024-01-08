using System.ComponentModel.DataAnnotations;

namespace ComplectPlus.Models
{
    public class Staff
    {
        public int StaffId { get; set; }

        [Required(ErrorMessage = "Укажите имя")]
        [Display(Name = "Имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]

        public string? Name { get; set; }//имя

        [Required(ErrorMessage = "Укажите фамилию")]
        [Display(Name = "Фамилия")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]

        public string? Surname { get; set; }//фамилия

        [Display(Name = "Должность")]
        public int? DoljnostId { get; set; }
        [Display(Name = "Должность")]
        public virtual Doljnost? Doljnosts { get; set; }
       // public virtual ICollection<Issuance>? Issuance { get; set; }
    }
}
