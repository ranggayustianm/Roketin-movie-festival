using MediatR;
using MovieFestival.Models;
using MovieFestival.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieFestival.Features.AllUsersFeatures.Queries
{
    public class SearchMoviesQuery : IRequest<IEnumerable<Movie>>
    {
        public string? SearchTerm { get; set; }
        public class SearchMoviesQueryHandler : IRequestHandler<SearchMoviesQuery, IEnumerable<Movie>>
        {
            private readonly IApplicationContext _context;
            public SearchMoviesQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Movie>> Handle(SearchMoviesQuery query, CancellationToken cancellationToken)
            {
                var searchTerm = query?.SearchTerm?.ToLower();
                var movies = await _context.Movies.ToListAsync(cancellationToken: cancellationToken);
                    return movies
                        .Where(movie => movie.Title.ToLower().Contains(searchTerm) ||
                            movie.Description.ToLower().Contains(searchTerm) ||
                            movie.Artists.Any(artist => artist.ToLower().Contains(searchTerm)) ||
                            movie.Genres.Any(genre => genre.ToLower().Contains(searchTerm)))
                        .ToList();
            }
        }
    }
}
