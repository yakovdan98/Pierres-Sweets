using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SweetShop.Models
{
  public class SweetShopContext : IdentityDbContext<ApplicationUser>
  {
    // include DbSets as needed
    public DbSet<Treat> Treats { get; set; }
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<TreatFlavor> TreatFlavors { get; set; }


    public SweetShopContext(DbContextOptions options) : base(options) { }
  }
}