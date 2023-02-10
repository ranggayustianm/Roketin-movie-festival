using MediatR;
using MovieFestival.Models;
using MovieFestival.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieFestival.Features.AdminFeatures.Queries
{
    public class GetMostViewedGenreQuery : IRequest<List<string>>
    {
        public class GetMostViewedGenreQueryHandler : IRequestHandler<GetMostViewedGenreQuery, List<string>>
        {
            private readonly IApplicationContext _context;
            public GetMostViewedGenreQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<List<string>> Handle(GetMostViewedGenreQuery query, CancellationToken cancellationToken)
            {
                var genres = _context.Movies
                    .AsEnumerable()
                    .SelectMany(movie => movie.Genres)
                    .GroupBy(genre => genre)
                    .Select(genre => new { 
                        Genre = genre.Key,
                        ViewCount = genre.Sum(x => _context.Movies.AsEnumerable().Where(y => y.Genres.Contains(genre.Key)).Sum(y => y.ViewCount)) 
                    });

                var viewCount = genres
                    .OrderByDescending(genre => genre.ViewCount)
                    .FirstOrDefault()
                    ?.ViewCount;

                return genres
                    .Where(genre => genre.ViewCount == viewCount)
                    .Select(item => item.Genre)
                    .ToList();
            }
        }
    }
}
