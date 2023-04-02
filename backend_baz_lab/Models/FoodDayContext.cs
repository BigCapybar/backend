using Microsoft.EntityFrameworkCore;
using backend_baz_lab.Models;

namespace backend_baz_lab.Models
{
    public class FoodDayContext : DbContext
    {
        public FoodDayContext(DbContextOptions<FoodDayContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<backend_baz_lab.Models.Meal> Meal { get; set; } = default!;
        public DbSet<backend_baz_lab.Models.FoodDay> FoodDay { get; set; } = default!;
        public DbSet<backend_baz_lab.Models.User> User { get; set; } = default!;

        }
}
