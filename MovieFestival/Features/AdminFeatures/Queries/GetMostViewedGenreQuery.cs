using MediatR;
using MovieFestival.Models;
using MovieFestival.Context;

namespace MovieFestival.Features.AdminFeatures.Queries
{
    public class GetMostViewedGenreQuery : IRequest<string>
    {
        public class GetMostViewedGenreQueryHandler : IRequestHandler<GetMostViewedGenreQuery, string>
        {
            private readonly IApplicationContext _context;
            public GetMostViewedGenreQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(GetMostViewedGenreQuery query, CancellationToken cancellationToken)
            {
                var genres = _context.Movies
                    .SelectMany(movie => movie.Genres)
                    .GroupBy(genre => genre)
                    .Select(genre => new { 
                        Genre = genre.Key,
                        ViewCount = genre.Sum(x => _context.Movies.Where(y => y.Genres.Contains(genre.Key)).Sum(y => y.ViewCount)) 
                    });

                return genres
                    .OrderByDescending(genre => genre.ViewCount)
                    .FirstOrDefault()
                    ?.Genre;
            }
        }
    }
}
