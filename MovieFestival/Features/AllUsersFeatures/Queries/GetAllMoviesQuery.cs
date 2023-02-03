using MediatR;
using MovieFestival.Models;
using MovieFestival.Context;
using Microsoft.EntityFrameworkCore;

namespace MovieFestival.Features.AllUsersFeatures.Queries
{
    public class GetAllMoviesQuery : IRequest<IEnumerable<Movie>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<Movie>>
        {
            private readonly IApplicationContext _context;
            public GetAllMoviesQueryHandler(IApplicationContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Movie>> Handle(GetAllMoviesQuery query, CancellationToken cancellationToken)
            {
                return await _context.Movies
                    .Skip((query.PageNumber - 1) * query.PageSize)
                    .Take(query.PageSize)
                    .OrderBy(key => key.Id)
                    .ToListAsync();
            }
        }
    }
}
