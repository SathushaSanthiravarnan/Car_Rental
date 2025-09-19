using Asp.Versioning;
using CarRentalSystem.Application.Commands.Car;
using CarRentalSystem.Application.DTOs.Car;
using CarRentalSystem.Application.Queries.Car;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.Car
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get a single car by Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CarDto?>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCarByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get a paged list of cars.
        /// </summary>
        /// <param name="page">1-based page number.</param>
        /// <param name="pageSize">Items per page.</param>
        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetCarsPagedQuery(page, pageSize), ct);
            return Ok(result);
        }

        /// <summary>
        /// Create a new car.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CarDto>> Create([FromBody] CreateCarDto dto, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreateCarCommand(dto), ct);
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(
                nameof(GetById),
                routeValues: new { version, id = created.Id },
                value: created
            );
        }

        /// <summary>
        /// Update an existing car.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CarDto>> Update(Guid id, [FromBody] UpdateCarDto dto, CancellationToken ct)
        {
            try
            {
                var updated = await _mediator.Send(new UpdateCarCommand(id, dto), ct);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Soft delete a car.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeleteCarCommand(id), ct);
            return success ? NoContent() : NotFound();
        }
    }
}
