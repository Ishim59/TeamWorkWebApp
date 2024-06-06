using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamWorkWebApp.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Executor { get; set; }
        [ForeignKey("Executor")]
        public User User { get; set; }
        [Required]
        public int Group { get; set; }
        [ForeignKey("Group")]
        public Group GroupObj { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

        [Required] public bool Status { get; set; } = false;
    }
}
