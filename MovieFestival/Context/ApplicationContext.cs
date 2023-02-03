using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(x =>
            {
                x.HasKey(y => y.Id);
                x.Property(y => y.Artists)
                    .HasConversion(
                        from => string.Join("|", from),
                        to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        new ValueComparer<List<string>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()
                    )
                );
                x.Property(y => y.Genres)
                    .HasConversion(
                        from => string.Join("|", from),
                        to => string.IsNullOrEmpty(to) ? new List<string>() : to.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList(),
                        new ValueComparer<List<string>>(
                            (c1, c2) => c1.SequenceEqual(c2),
                            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                            c => c.ToList()
                    )
                );
            });
        }
    }
}
