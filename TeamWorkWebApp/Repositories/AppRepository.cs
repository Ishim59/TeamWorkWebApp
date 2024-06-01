using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TeamWorkWebApp.Interfaces;
using TeamWorkWebApp.Models;
using Task = TeamWorkWebApp.Models.Task;

namespace TeamWorkWebApp.Repositories;

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

    public async Task<IEnumerable<Task>> GetTasksAsync()
    {
        return await _context.Tasks.ToListAsync().ConfigureAwait(false);
    }

    public async Task<bool> UserExistsAsync(string email, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password)
            .ConfigureAwait(false) != null;
    }

    //public async Task<bool> EmailExist(string email)
    //{
    //    return await _context.Users.FirstOrDefaultAsync(u=> u.Email == email) != null;
    //}
    public async Task<bool> AddUserAsync(string email, string password, string name)
    {
        if (!await _context.Users.AnyAsync(u => u.Email == email).ConfigureAwait(false))
            _context.Users.Add(new User { Email = email, Password = password, Name = name });
        return await System.Threading.Tasks.Task.FromResult(Save()).ConfigureAwait(false);
    }

    public Task<User?> GetUserByIdAsync(int userId) => _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

    public Task<Group?> GetGroupByIdAsync(int groupId) => _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);

    public async Task<IEnumerable<Group>> GetGroupsByUserAsync(int userId)
    {
        var result = new List<Group>();
        var groups = (await GetGroupsAsync().ConfigureAwait(false)).ToList();
        foreach (var group in groups)
        {
            var jObject = group.GetJson();
            var jArray = (JArray)jObject["Members"]!;
            foreach (var item in jArray)
                if (item.Value<int>() == userId)
                {
                    result.Add(group);
                    break;
                }
        }

        return result;
    }

    public async Task<IEnumerable<Task>> GetTasksOfExecutorAsync(User user, Group group)
    {
        return await _context.Tasks.Where(t => t.User == user && t.GroupObj == group).ToListAsync()
            .ConfigureAwait(false);
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