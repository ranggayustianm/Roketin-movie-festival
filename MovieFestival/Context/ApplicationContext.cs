using Microsoft.EntityFrameworkCore;
using MovieFestival.Models;

namespace MovieFestival.Context
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<Movie> Movies { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
