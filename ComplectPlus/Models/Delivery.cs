using System.ComponentModel.DataAnnotations;

namespace ComplectPlus.Models
{//поставка, доставка
    public class Delivery
    {

        public int DeliveryId { get; set; }

        [Display(Name = "Поставщик")]
        public int SellerId { get; set; }//поставщик

        [Display(Name = "Дата поставки")]
        public DateTime DeliveryDate { get; set; }//дата поставки

        [Display(Name = "Комплектующие")]
        public int ComponentId { get; set; }//компонент

        [Required(ErrorMessage = "Укажите количество")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }//количество

        [Display(Name = "Поставщик")]
        public virtual Seller? Seller { get; set; }

        public virtual ICollection<Сomponent>? Сomponents { get; set; }
    }
}




