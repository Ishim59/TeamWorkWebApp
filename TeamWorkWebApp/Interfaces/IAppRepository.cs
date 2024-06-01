using TeamWorkWebApp.Models;

namespace TeamWorkWebApp.Interfaces
{
    public interface IAppRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<IEnumerable<Models.Task>> GetTasksAsync();
        Task<bool> UserExists(string email, string password);
        Task<bool> AddUser(string email, string password, string name);


        bool Save();
    }
}
