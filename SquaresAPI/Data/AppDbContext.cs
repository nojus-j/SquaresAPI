using Microsoft.EntityFrameworkCore;
using SquaresAPI.Models;

namespace SquaresAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<PointList> PointLists { get; set; }
        public DbSet<Point> Points { get; set; }
    }
}
