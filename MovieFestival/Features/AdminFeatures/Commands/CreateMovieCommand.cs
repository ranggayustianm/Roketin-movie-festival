using MediatR;
using MovieFestival.Models;
using MovieFestival.Context;

namespace MovieFestival.Features.AdminFeatures.Commands
{
    public class CreateMovieCommand : IRequest<int>
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? Artists { get; set; }
        public List<string>? Genres { get; set; }
        public string? WatchUrl { get; set; }

        public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, int>
        {
            private readonly IApplicationContext _context;
            public CreateMovieCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(CreateMovieCommand command, CancellationToken cancellationToken)
            {
                var movie = new Movie()
                {
                    Title = command.Title,
                    Description = command.Description,
                    Artists = command.Artists,
                    Genres = command.Genres,
                    WatchUrl = command.WatchUrl
                };
                _context.Movies.Add(movie);
                await _context.SaveChanges();
                return movie.Id;
            }
        }
    }
}
