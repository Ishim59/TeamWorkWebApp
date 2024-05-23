using TeamWorkWebApp.Models;

namespace TeamWorkWebApp.Interfaces
{
    public interface IAppRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<IEnumerable<Models.Task>> GetTasksAsync();





    }
}
