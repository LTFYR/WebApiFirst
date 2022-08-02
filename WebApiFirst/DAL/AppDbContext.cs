using Microsoft.EntityFrameworkCore;
using WebApiFirst.Models;

namespace WebApiFirst.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Computer> Computers { get; set; }
    }
}
