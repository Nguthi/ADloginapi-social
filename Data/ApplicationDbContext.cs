using Microsoft.EntityFrameworkCore;
using ADloginAPI.Models;
namespace ADloginAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 

        }
        public DbSet<User> Users { get; set; }
    }

    
}
