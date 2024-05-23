using Microsoft.EntityFrameworkCore;
using TeamWorkWebApp.Interfaces;
using TeamWorkWebApp.Models;

namespace TeamWorkWebApp.Repositories
{
    public class AppRepository : IAppRepository
    {
        private readonly AppDbContext _context;
        public AppRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<IEnumerable<Models.Task>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
