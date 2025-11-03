using Microsoft.EntityFrameworkCore;
using VideoGameCatalog.Models;

namespace VideoGameCatalog.Repository.DbContext
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Game { get; set; }
    }
}
