using TeamWorkWebApp.Models;

namespace TeamWorkWebApp.ViewModels
{
    public class GroupsViewModel
    {
        public int Id { get; set; }
        public List<Group>? Groups { get; set; }
        public int SelectedGroupId { get; set; }
        public string CurrentTitle { get; set; }
        public string CurrentDescription { get; set; }
        public List<int> CurrentMembers { get; set; }
        public string NewTitle { get; set; }
        public string NewDescription { get; set; }
        public List<int> NewMembers { get; set; }
    }
}
