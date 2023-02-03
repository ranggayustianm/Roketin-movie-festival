using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieFestival.Features.AdminFeatures.Commands;
using MovieFestival.Features.AdminFeatures.Queries;

namespace MovieFestival.Controllers
{
    [Authorize]
    public class MovieAdminController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> Create(CreateMovieCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateMovieCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        public async Task<IActionResult> GetMostViewedMovie()
        {
            return Ok(await Mediator.Send(new GetMostViewedMovieQuery()));
        }

        public async Task<IActionResult> GetMostViewedGenre()
        {
            return Ok(await Mediator.Send(new GetMostViewedGenreQuery()));
        }
    }
}
