using TeamWorkWebApp.Models;
using Task = TeamWorkWebApp.Models.Task;

namespace TeamWorkWebApp.Interfaces
{
    public interface IAppRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<IEnumerable<Models.Task>> GetTasksAsync();
        Task<IEnumerable<Group>> GetGroupsByUserAsync(int userId);
        Task<IEnumerable<Task>> GetTasksOfExecutorAsync(User user, Group group);
        Task<bool> UserExistsAsync(string email, string password);
        Task<bool> AddUserAsync(string email, string password, string name);
        Task<User?> GetUserByIdAsync(int userId);
        Task<Group?> GetGroupByIdAsync(int groupId);
        bool Save();
    }
}
