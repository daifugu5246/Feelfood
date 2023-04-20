using Feelfood.Models;
using Microsoft.EntityFrameworkCore;

namespace Feelfood.Data
{
    public class FeelfoodDbContext : DbContext
    {
        public FeelfoodDbContext(DbContextOptions<FeelfoodDbContext> options): base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }
}
