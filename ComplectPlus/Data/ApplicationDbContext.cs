using ComplectPlus.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace ComplectPlus.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {}
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomUser> CustomUsers { get; set; } 
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Doljnost> Doljnosts { get; set; }
        public DbSet<Issuance> Issuances { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Сomponent> Components { get; set; }
        public DbSet<StorageOtch> StorageOtch { get; }

    }
}




