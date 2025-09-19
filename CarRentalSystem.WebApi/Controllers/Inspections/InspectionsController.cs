using Asp.Versioning;
using CarRentalSystem.Application.Commands.Inspection;
using CarRentalSystem.Application.DTOs.Inspection;
using CarRentalSystem.Application.Queries.Inspection;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.Inspections
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class InspectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InspectionsController(IMediator mediator) => _mediator = mediator;

        /// <summary>Get a single inspection by Id.</summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InspectionDto>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetInspectionByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Get a paginated list of all inspections.</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 1;
            if (pageSize > 200) pageSize = 200;

            var result = await _mediator.Send(new GetInspectionsPagedQuery(page, pageSize), ct);
            return Ok(result);
        }

        /// <summary>Creates a new inspection.</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InspectionDto>> Create([FromBody] CreateInspectionDto dto, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreateInspectionCommand(dto), ct);
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(
                nameof(GetById),
                routeValues: new { version, id = created.Id },
                value: created
            );
        }

        /// <summary>Update an existing inspection.</summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<InspectionDto>> Update(Guid id, [FromBody] UpdateInspectionDto dto, CancellationToken ct)
        {
            var updated = await _mediator.Send(new UpdateInspectionCommand(id, dto), ct);
            return Ok(updated);
        }

        /// <summary>Soft deletes an inspection (marks as IsDeleted).</summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeleteInspectionCommand(id), ct);
            return success ? NoContent() : NotFound();
        }
    }
}
