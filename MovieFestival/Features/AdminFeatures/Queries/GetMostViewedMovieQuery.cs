using MediatR;
using MovieFestival.Models;
using MovieFestival.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieFestival.Features.AdminFeatures.Queries
{
    public class GetMostViewedMovieQuery : IRequest<Movie>
    {
        public class GetMostViewedMovieQueryHandler : IRequestHandler<GetMostViewedMovieQuery, Movie>
        {
            private readonly IApplicationContext _context;
            public GetMostViewedMovieQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<Movie> Handle(GetMostViewedMovieQuery query, CancellationToken cancellationToken)
            {
                return await _context.Movies
                    .OrderByDescending(movie => movie.ViewCount)
                    .FirstOrDefaultAsync();
            }
        }
    }
}
