namespace TeamWorkWebApp.ViewModels
{
    public class TasksViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public List<Models.Task> Tasks { get; set; }
        public int SelectedTaskId { get; set; }
        public Models.Task SelectedTask { get; set; }
    }
}
