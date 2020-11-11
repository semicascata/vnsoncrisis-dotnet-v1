using Microsoft.EntityFrameworkCore;
using VenusOnCrisis.Entities;

namespace VenusOnCrisis.Data
{
    public class DataContext : DbContext
    {
        // db configuration [sql server]
        public DataContext(DbContextOptions options) : base (options) {}

        // entities/schemas
        public DbSet<User> Users { get; set; }
    }
}