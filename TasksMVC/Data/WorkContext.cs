using TasksMVC.Models;
using Microsoft.EntityFrameworkCore;
using Work = TasksMVC.Models.Work;

namespace TasksMVC.Data
{
    public class WorkContext : DbContext
    {
        public WorkContext(DbContextOptions<WorkContext> options)
           : base(options)
        {
        }
        public DbSet<Work> Works { get; set; } = null!;

       
    }
}
