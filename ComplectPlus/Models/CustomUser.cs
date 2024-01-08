using Microsoft.AspNetCore.Identity;

namespace ComplectPlus.Models
{
    public class CustomUser : IdentityUser
    {
        public string? Surname { get; set; }

        public string? Ima { get; set; }

        public string? Secsurname { get; set; }

        public int? Age { get; set; }

        public int? PositionId { get; set; }

        public virtual Position? Positions { get; set; }
    }
}
