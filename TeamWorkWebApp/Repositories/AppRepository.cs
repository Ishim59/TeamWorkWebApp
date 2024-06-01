using Microsoft.EntityFrameworkCore;
using TeamWorkWebApp.Interfaces;
using TeamWorkWebApp.Models;
using Task = System.Threading.Tasks.Task;

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

        public async Task<bool> UserExists(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password) != null;
        }

        public Task<bool> AddUser(string email, string password, string name)
        {
            _context.Users.Add(new User() { Email = email, Password = password, Name = name });
            return Task.FromResult(Save());
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
