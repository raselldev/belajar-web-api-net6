using BelajarWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BelajarWebApi.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
