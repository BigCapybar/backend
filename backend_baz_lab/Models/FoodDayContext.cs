using Microsoft.EntityFrameworkCore;
using backend_baz_lab.Models;

namespace backend_baz_lab.Models
{
    public class FoodDayContext : DbContext
    {
        public FoodDayContext(DbContextOptions<FoodDayContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FoodDay>().HasMany(fd => fd.Meals).WithMany();
        }
        public DbSet<backend_baz_lab.Models.Meal> Meal { get; set; } = default!;
        public DbSet<backend_baz_lab.Models.FoodDay> FoodDay { get; set; } = default!;
        public DbSet<backend_baz_lab.Models.User> User { get; set; } = default!;

        }
}
