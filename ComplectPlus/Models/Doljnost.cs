namespace ComplectPlus.Models
{
    public class Doljnost
    {
        public int DoljnostId { get; set; }
        public string? DoljnostName { get; set; }
        public virtual ICollection<Staff>? Staffs { get; set; }
    }
}
