using MediatR;
using MovieFestival.Context;
using System.Text.Json.Serialization;

namespace MovieFestival.Features.AdminFeatures.Commands
{
    public class UpdateMovieCommand : IRequest<int>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? Artists { get; set; }
        public List<string>? Genres { get; set; }
        public string? WatchUrl { get; set; }

        public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, int>
        {
            private readonly IApplicationContext _context;
            public UpdateMovieCommandHandler(IApplicationContext context)
            {
                _context = context;
            }
            public async Task<int> Handle(UpdateMovieCommand command, CancellationToken cancellationToken)
            {
                var movie = _context.Movies.Where(movie => movie.Id == command.Id).FirstOrDefault();
                if (movie == null)
                {
                    return -1;
                }

                movie.Title = command.Title;
                movie.Description = command.Description;
                movie.Artists = command.Artists;
                movie.Genres = command.Genres;
                movie.WatchUrl = command.WatchUrl;

                await _context.SaveChanges();
                return movie.Id;
            }
        }
    }
}
