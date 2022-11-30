using System.ComponentModel.DataAnnotations;

namespace BelajarWebApi.Models
{
    public class User
    {
        [Key]
        public long Id { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(150)]
        public string Password { get; set; }
        [StringLength(50)]
        public string Fullname { get; set; }

        public ICollection<Todo> Todos { get; set; }
    }
}
