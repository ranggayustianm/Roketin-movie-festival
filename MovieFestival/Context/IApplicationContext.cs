using MovieFestival.Models;

namespace MovieFestival.Context
{
    public interface IApplicationContext
    {
        DbSet<Movie> Movies { get; set; }

        Task<int> SaveChanges();
    }
}