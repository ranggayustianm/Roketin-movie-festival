using MediatR;
using MovieFestival.Context;

namespace MovieFestival.Features.AllUsersFeatures.Queries
{
    public class WatchMovieQuery : IRequest<bool>
    {
        public int MovieId { get; set; }

        public class WatchMovieQueryHandler : IRequestHandler<WatchMovieQuery, bool>
        {
            private readonly IApplicationContext _context;
            public WatchMovieQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<bool> Handle(WatchMovieQuery query, CancellationToken cancellationToken)
            {
                var movie = _context.Movies.Where(movie => movie.Id == query.MovieId).FirstOrDefault();
                if (movie == null) return false;

                movie.ViewCount++;
                await _context.SaveChanges();

                return true;
            }
        }
    }
}
