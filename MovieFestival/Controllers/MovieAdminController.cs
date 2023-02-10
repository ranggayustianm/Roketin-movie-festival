using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieFestival.Features.AdminFeatures.Commands;
using MovieFestival.Features.AdminFeatures.Queries;
using MovieFestival.Utils;

namespace MovieFestival.Controllers
{
    [ApiController]
    [Route("api/admin/[action]")]
    public class MovieAdminController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(new ResponseDto(result, $"New Movie with ID {result} successfully created"));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMovieCommand command)
        {
            command.Id = id;

            var result = await Mediator.Send(command);
            if (result == -1) return BadRequest(new ResponseDto(result, $"Movie ID is invalid: {id}", 400));

            return Ok(new ResponseDto(result, $"Movie {id} successfully updated"));
        }

        [HttpGet]
        public async Task<IActionResult> GetMostViewedMovie()
        {
            var result = await Mediator.Send(new GetMostViewedMovieQuery());
            return Ok(new ResponseDto(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetMostViewedGenre()
        {
            var result = await Mediator.Send(new GetMostViewedGenreQuery());
            return Ok(new ResponseDto(result));
        }
    }
}
