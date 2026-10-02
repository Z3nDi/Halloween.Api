using Microsoft.EntityFrameworkCore;
using Halloween.Api.Models;

namespace Halloween.Api.Data
{
    public class MonsterDbContext : DbContext
    {
        public MonsterDbContext(DbContextOptions<MonsterDbContext> options)
            : base(options)
        {
        }
        public DbSet<Monster> Monsters { get; set; }
    }
}