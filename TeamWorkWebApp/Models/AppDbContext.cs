using Microsoft.EntityFrameworkCore;

namespace TeamWorkWebApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Task> Tasks { get; set; }

    }
}
