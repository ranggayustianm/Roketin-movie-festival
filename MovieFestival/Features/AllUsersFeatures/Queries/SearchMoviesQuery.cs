using MediatR;
using MovieFestival.Models;
using MovieFestival.Context;

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
                return await _context.Movies
                    .Where(movie => movie.Title.Contains(query.SearchTerm) ||
                        movie.Description.Contains(query.SearchTerm) ||
                        movie.Artists.Any(artist => artist.Contains(query.SearchTerm)) ||
                        movie.Genres.Any(genre => genre.Contains(query.SearchTerm)))
                    .ToListAsync();
            }
        }
    }
}
