using Asp.Versioning;
using CarRentalSystem.Application.Commands.Users;
using CarRentalSystem.Application.DTOs.User;
using CarRentalSystem.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.User
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        /// <summary>Get a single user by Id.</summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto?>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Get paged users.</summary>
        /// <param name="page">1-based page number</param>
        /// <param name="pageSize">items per page (1..200)</param>
        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 1;
            if (pageSize > 200) pageSize = 200;

            var result = await _mediator.Send(new GetUsersPagedQuery(page, pageSize), ct);
            return Ok(result);
        }

        /// <summary>Create a new user.</summary>
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserDto dto, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreateUserCommand(dto), ct);

            // Honor API version in route (falls back to requested version or "1.0")
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(
                nameof(GetById),
                routeValues: new { version, id = created.Id },
                value: created
            );
        }

        /// <summary>Update an existing user.</summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UpdateUserDto dto, CancellationToken ct)
        {
            var updated = await _mediator.Send(new UpdateUserCommand(id, dto), ct);
            return Ok(updated);
        }

        /// <summary>Soft delete a user (marks IsDeleted = true).</summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeleteUserCommand(id), ct);
            return success ? NoContent() : NotFound();
        }
    }
}
