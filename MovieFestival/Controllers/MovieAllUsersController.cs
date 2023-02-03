using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieFestival.Features.AllUsersFeatures.Queries;

namespace MovieFestival.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class MovieAllUsersController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return Ok(await Mediator.Send(new GetAllMoviesQuery { PageNumber = pageNumber, PageSize = pageSize }));
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchMoviesQuery { SearchTerm = searchTerm }));
        }

        [HttpGet]
        public async Task<IActionResult> TrackViewership([FromQuery] int movieId)
        {
            return Ok(await Mediator.Send(new TrackViewershipQuery { MovieId = movieId }));
        }
    }
}
