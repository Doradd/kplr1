using System.ComponentModel.DataAnnotations;

namespace ComplectPlus.Models
{
    public class Storage
    {
        public int StorageId { get; set; }

        [Display(Name = "Комплектующие")]
        public int ComponentId { get; set; }//компонент

        [Required(ErrorMessage = "Укажите количество")]
        [Display(Name = "Количество")]
        //[RegularExpression(@"\d{6}", ErrorMessage = "Некорректное количество")]
        public int Quantity { get; set; }//количество

        public virtual ICollection<Сomponent>? Сomponents { get; set; }
    }
}
