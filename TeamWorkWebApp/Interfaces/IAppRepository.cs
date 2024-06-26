﻿using TeamWorkWebApp.Models;
using Task = TeamWorkWebApp.Models.Task;

namespace TeamWorkWebApp.Interfaces
{
    public interface IAppRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<IEnumerable<Models.Task>> GetTasksAsync();
        Task<IEnumerable<Group>> GetGroupsByUserAsync(int userId);
        Task<List<Task>> GetTasksOfExecutorAsync(User user, Group group);
        Task<bool> UserExistsAsync(string email, string password);
        Task<bool> AddUserAsync(string email, string password, string name);
        Task<int> GetUserIdAsync (string email);
        Task<User?> GetUserByIdAsync(int userId);
        Task<bool> AddGroupAsync(string teamLead, string title, string description);
        Task<Group?> GetGroupByIdAsync(int groupId);
        Task<IEnumerable<User>> GetGroupMembersAsync(int groupId);
        Task<bool> AddTaskAsync(int executor, int groupId, string title, string description);
        bool Save();
    }
}
