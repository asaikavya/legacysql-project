using Legacysql.Models;
using Microsoft.EntityFrameworkCore;

namespace Legacysql.Data 
{
    public class AppDbContext : DbContext
    {
        // This constructor passes the "options" (like the connection string) to the base class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Project> Projects { get; set; }

    }
}