using Microsoft.EntityFrameworkCore;

namespace TeamWorkWebApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
             
        }
    }
}
