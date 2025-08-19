using AutoMapper;
using Forum.Application.Features.AccountFeatures.Commands.ChangePassword;
using Forum.Application.Features.AccountFeatures.Commands.Registration;
using Forum.Application.Features.AccountFeatures.Commands.ResetPassword.NewPassword;
using Forum.Application.Features.AccountFeatures.Commands.ResetPassword.SendOtp;
using Forum.Application.Features.AccountFeatures.Commands.ResetPassword.Validate;
using Forum.Application.Features.AccountFeatures.Queries.Login;
using Forum.Application.Features.AccountFeatures.Queries.Refresh;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Forum.Api.Controllers.v1
{
    [ApiController]
    [Route("api/v{apiVersion:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("login-user")]
        public async Task<IActionResult> LoginUser([FromBody] LoginQuery loginQuery, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(loginQuery).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("register")]
        [SwaggerResponse(200, "User created successfully")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken)
                .ConfigureAwait(false);
            return Ok(new
            {
                Message = "The code has been successfully sent to the respective email."
            });
        }

        [HttpPost("validate")]
        public async Task<ActionResult> ValidateOtp([FromBody] ValidateOtpCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(new
            {
                ResetToken = result
            });
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);
            return Ok(new
            {
                Message = "Password reset successfully!"
            });
        }
    }
}
