using Microsoft.EntityFrameworkCore;
using TaskTimelinePlanner.Models;

namespace TaskTimelinePlanner.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TaskRequest> Tasks { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
