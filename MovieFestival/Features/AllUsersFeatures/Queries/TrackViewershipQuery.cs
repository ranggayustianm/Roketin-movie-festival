using MediatR;
using MovieFestival.Models;
using MovieFestival.Context;

namespace MovieFestival.Features.AllUsersFeatures.Queries
{
    public class TrackViewershipQuery : IRequest<int>
    {
        public int MovieId { get; set; }

        public class TrackViewershipQueryHandler : IRequestHandler<TrackViewershipQuery, int>
        {
            private readonly IApplicationContext _context;
            public TrackViewershipQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<int> Handle(TrackViewershipQuery query, CancellationToken cancellationToken)
            {
                var movie = _context.Movies.Where(movie => movie.Id == query.MovieId).FirstOrDefault();
                if (movie == null) return -1;
                return movie.ViewCount;
            }
        }
    }
}
