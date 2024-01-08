using System.ComponentModel.DataAnnotations;

namespace ComplectPlus.Models
{
    public class Position
    {
        public int? PositionId { get; set; }

        [Display(Name = "Должность")]
        public string? PositionName { get; set; }

        public virtual ICollection<CustomUser>? CustomUsers { get; set; }
    }
}
