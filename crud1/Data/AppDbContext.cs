using Microsoft.EntityFrameworkCore;

using crud1.Models;

namespace crud1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
