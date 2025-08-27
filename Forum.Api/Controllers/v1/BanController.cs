using AutoMapper;
using Forum.Application.Common.Dtos.BanInfo.Requests;
using Forum.Application.Features.AdminFeatures.Commands.BanUser;
using Forum.Application.Features.AdminFeatures.Commands.UpdateBan;
using Forum.Application.Features.AdminFeatures.Queries.GetAllBans;
using Forum.Application.Features.AdminFeatures.Queries.GetBanById;
using Forum.Application.Features.BanFeatures.Commands.UnbanUser;
using Forum.Domain.Parameters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Api.Controllers.v1
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/v{version:apiVersion}/bans")]
    public class BanController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BanController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("block")]
        public async Task<IActionResult> BanUser([FromBody] BanUserCommand command, CancellationToken cancellationToken)
        {
            var info = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(info);
        }

        [HttpPut("{banId}")]
        public async Task<IActionResult> UpdateBanInfo(int userId, int banId, [FromBody] UpdateBanInfo request, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<UpdateBanCommand>(request);
            command.Id = banId;
            command.UserId = userId;
            var result = await _mediator.Send(command, cancellationToken)
                .ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet("{banId}")]
        public async Task<IActionResult> GetBanById(int banId, CancellationToken cancellationToken)
        {
            var query = new GetBanByIdQuery
            {
                Id = banId
            };
            var response = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBans([FromQuery] RequestParameters parameters, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetAllBansQuery(parameters), cancellationToken)
                .ConfigureAwait(false);
            return Ok(response);
        }

        [HttpDelete("{banId}")]
        public async Task<IActionResult> DeleteBan(int banId, string userId, CancellationToken cancellationToken)
        {
            var command = new DeleteBanCommand
            {
                BanId = banId,
                UserId = userId
            };
            await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return NoContent();
        }
    }
}
