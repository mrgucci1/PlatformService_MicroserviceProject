using Microsoft.EntityFrameworkCore;
using PlatformService_MicroserviceProject.Models;
namespace PlatformService_MicroserviceProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }
        public DbSet<Platform> Platforms { get; set; }
    }
}
