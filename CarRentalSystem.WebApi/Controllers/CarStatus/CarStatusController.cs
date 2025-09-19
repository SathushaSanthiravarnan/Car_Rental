using Asp.Versioning;
using CarRentalSystem.Application.Commands.CarStatus;
using CarRentalSystem.Application.DTOs.CarStatus;
using CarRentalSystem.Application.Queries.CarStatus;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.CarStatus
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CarStatusesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarStatusesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get a single car status by Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CarStatusDto?>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCarStatusByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get a paged list of car statuses.
        /// </summary>
        /// <param name="page">1-based page number.</param>
        /// <param name="pageSize">Items per page.</param>
        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetCarStatusesPagedQuery(page, pageSize), ct);
            return Ok(result);
        }

        /// <summary>
        /// Create a new car status.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CarStatusDto>> Create([FromBody] CreateCarStatusDto dto, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreateCarStatusCommand(dto), ct);
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(
                nameof(GetById),
                routeValues: new { version, id = created.Id },
                value: created
            );
        }

        /// <summary>
        /// Update an existing car status.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CarStatusDto>> Update(Guid id, [FromBody] UpdateCarStatusDto dto, CancellationToken ct)
        {
            try
            {
                var updated = await _mediator.Send(new UpdateCarStatusCommand(id, dto), ct);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Soft delete a car status.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeleteCarStatusCommand(id), ct);
            return success ? NoContent() : NotFound();
        }
    }
}
