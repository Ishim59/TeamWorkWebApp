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
            return await _context.Groups.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Models.Task>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> UserExistsAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password).ConfigureAwait(false) != null;
        }
        //public async Task<bool> EmailExist(string email)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u=> u.Email == email) != null;
        //}
        public async Task<bool> AddUserAsync(string email, string password, string name)
        {
            if(!await _context.Users.AnyAsync(u => u.Email == email).ConfigureAwait(false))
                _context.Users.Add(new User() { Email = email, Password = password, Name = name });
            return await Task.FromResult(Save()).ConfigureAwait(false);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync().ConfigureAwait(false);
        }
    }
}
