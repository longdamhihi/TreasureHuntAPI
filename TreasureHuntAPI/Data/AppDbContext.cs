using Microsoft.EntityFrameworkCore;
using TreasureHuntAPI.Models;

namespace TreasureHuntAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<TreasureMap> TreasureMaps { get; set; }
    }
}