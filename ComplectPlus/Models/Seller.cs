using System.ComponentModel.DataAnnotations;

namespace ComplectPlus.Models
{ //поставщик
    public class Seller
    {
        public int SellerId { get; set; }

        [Required(ErrorMessage = "Укажите название")]
        [Display(Name = "Название поставщика")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string? SellerName { get; set; }//название

        [Display(Name = "Юридическое название")]
        public string? SellerUrName { get; set; }//юр название

        [Required(ErrorMessage = "Укажите адрес")]
        [Display(Name = "Адрес")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string? SellerAdress { get; set; }  //адрес
       // public virtual ICollection<Delivery>? Delivery { get; set; }
    }
}
