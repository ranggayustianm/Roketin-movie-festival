using Microsoft.EntityFrameworkCore;

namespace MovieFestival.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? Artists { get; set; }
        public List<string>? Genres { get; set; }
        public string? WatchUrl { get; set; }
        public int ViewCount { get; set; } = 0;
    }

}
