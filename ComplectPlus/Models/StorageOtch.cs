using Microsoft.EntityFrameworkCore;

namespace ComplectPlus.Models
{
    [Keyless]
    public class StorageOtch
    {
        public string? nm { get; set; }
        public string? kol { get; set; }
    }
}
