using Asp.Versioning;
using CarRentalSystem.Application.Commands.Manufacturer;
using CarRentalSystem.Application.DTOs.Manufacturer;
using CarRentalSystem.Application.Queries.Manufacturer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.Manufacturer
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class ManufacturersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManufacturersController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get a single manufacturer by Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ManufacturerDto?>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetManufacturerByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get a paged list of manufacturers.
        /// </summary>
        /// <param name="page">1-based page number.</param>
        /// <param name="pageSize">Items per page (1-200).</param>
        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetManufacturersPagedQuery(page, pageSize), ct);
            return Ok(result);
        }

        /// <summary>
        /// Create a new manufacturer.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ManufacturerDto>> Create([FromBody] CreateManufacturerDto dto, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreateManufacturerCommand(dto), ct);
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(
                nameof(GetById),
                routeValues: new { version, id = created.Id },
                value: created
            );
        }

        /// <summary>
        /// Update an existing manufacturer.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ManufacturerDto>> Update(Guid id, [FromBody] UpdateManufacturerDto dto, CancellationToken ct)
        {
            var updated = await _mediator.Send(new UpdateManufacturerCommand(id, dto), ct);
            return Ok(updated);
        }

        /// <summary>
        /// Soft delete a manufacturer (sets IsDeleted to true).
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeleteManufacturerCommand(id), ct);
            return success ? NoContent() : NotFound();
        }
    }
}
