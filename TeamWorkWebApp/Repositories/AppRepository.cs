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

    public async Task<int> GetUserIdAsync(string email)
    {
        return (await _context.Users.FirstOrDefaultAsync(u => u.Email == email).ConfigureAwait(false))!.Id;
    }

    public Task<User> GetUserByIdAsync(int userId) => _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

    public async Task<bool> AddGroupAsync(string teamLead, string title, string description)
    {
        await System.Threading.Tasks.Task.Run(() =>
        {
            _context.Groups.Add(new Group { TeamLead = teamLead, Title = title, Description = description });
        }).ConfigureAwait(false);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        var lastGroup = _context.Groups.OrderBy(g => g.Id).Last();
        var jObject = lastGroup.GetJson();
        var jArray = (JArray)jObject["Members"]!;
        jArray.Add(int.Parse(lastGroup.TeamLead));
        _context.Groups.OrderBy(g => g.Id).Last().PutJson(jObject);
        return Save();
    }

    public Task<Group> GetGroupByIdAsync(int groupId) => _context.Groups.FirstOrDefaultAsync(g => g.Id == groupId);
    public async Task<IEnumerable<User>> GetGroupMembersAsync(int groupId)
    {
        var members = new List<User>();
        var group = await GetGroupByIdAsync(groupId).ConfigureAwait(false);
        var jObject = group!.GetJson();
        var jArray = (JArray)jObject["Members"];
        foreach (var item in jArray)
        {
            members.Add(await GetUserByIdAsync(item.Value<int>()).ConfigureAwait(false)!);
        }
        return members;
    }

    public async Task<bool> AddTaskAsync(int executor, int groupId, string title, string description)
    {
        _context.Tasks.Add(new Task()
        {
            User = await GetUserByIdAsync(executor).ConfigureAwait(false),
            GroupObj = await GetGroupByIdAsync(groupId).ConfigureAwait(false),
            Title = title,
            Description = description
        });
        return Save();
    }

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

    public async Task<List<Task>> GetTasksOfExecutorAsync(User user, Group group)
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