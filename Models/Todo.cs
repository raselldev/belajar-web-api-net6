using System.ComponentModel.DataAnnotations;

namespace BelajarWebApi.Models
{
    public class Todo
    {
        [Key]
        public long Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(300)]
        public string Note { get; set; }
        public bool IsCompleted { get; set; }
        public long UserId { get; set; }

        public User User { get; set; }
    }
}
