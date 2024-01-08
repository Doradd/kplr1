using System.ComponentModel.DataAnnotations;

namespace ComplectPlus.Models
{    //выдача
    public class Issuance
    {
        public int IssuanceId { get; set; }

        [Display(Name = "Комплектующие")]
        public int ComponentId { get; set; }//компонент

        [Display(Name = "Ответсвенный")]
        public int StaffId { get; set; }//ответсвтенный (сотрудник)

        [Display(Name = "Дата выдачи")]
        public DateTime DateIssuance { get; set; }//дата выдачи

        [Required(ErrorMessage = "Укажите количество")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }//количество
        [Display(Name = "Ответсвенный")]
        public virtual Staff? Staffs { get; set; }
        public virtual ICollection<Сomponent>? Сomponents { get; set; }
    }
}









