using TeamWorkWebApp.Models;

namespace TeamWorkWebApp.ViewModels
{
    public class GroupsViewModel
    {
        public int Id { get; set; }
        public List<Group>? Groups { get; set; }
        public int SelectedGroupId { get; set; }
        
        public Group? SelectedGroup { get; set; }

        public List<User> SelectedGroupMembers { get; set; }
    }
}
