using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieFestival.Features.AllUsersFeatures.Queries;
using MovieFestival.Utils;

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
            var result = await Mediator.Send(new GetAllMoviesQuery { PageNumber = pageNumber, PageSize = pageSize });
            return Ok(new ResponseDto(result));
        }

        [HttpGet("{searchTerm}")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var result = await Mediator.Send(new SearchMoviesQuery { SearchTerm = searchTerm });
            return Ok(new ResponseDto(result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> WatchMovie(int id)
        {
            var result = await Mediator.Send(new WatchMovieQuery { MovieId = id });
            if (result == false) return BadRequest(new ResponseDto(result, $"Movie ID is invalid: {id}", 400));
            return Ok(new ResponseDto(result, $"Movie {id} is ready to be watched!"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TrackViewership(int id)
        {
            var result = await Mediator.Send(new TrackViewershipQuery { MovieId = id });
            if (result == -1) return BadRequest(new ResponseDto(result, $"Movie ID is invalid: {id}", 400));
            return Ok(new ResponseDto(result));
        }
    }
}
