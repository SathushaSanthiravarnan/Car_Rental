using Asp.Versioning;
using CarRentalSystem.Application.Commands.CarCategory;
using CarRentalSystem.Application.Commands.CarModel;
using CarRentalSystem.Application.DTOs.CarCategory;
using CarRentalSystem.Application.DTOs.CarModelDto;
using CarRentalSystem.Application.Queries.CarCategory;
using CarRentalSystem.Application.Queries.CarModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.CarModel
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CarModelsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarModelsController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Get a single car model by Id.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CarModelDto?>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCarModelByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>
        /// Get a paged list of car models.
        /// </summary>
        /// <param name="page">1-based page number.</param>
        /// <param name="pageSize">Items per page (1-200).</param>
        [HttpGet]
        public async Task<IActionResult> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetCarModelsPagedQuery(page, pageSize), ct);
            return Ok(result);
        }

        /// <summary>
        /// Create a new car model.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<CarModelDto>> Create([FromBody] CreateCarModelDto dto, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreateCarModelCommand(dto), ct);
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(
                nameof(GetById),
                routeValues: new { version, id = created.Id },
                value: created
            );
        }

        /// <summary>
        /// Update an existing car model.
        /// </summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CarModelDto>> Update(Guid id, [FromBody] UpdateCarModelDto dto, CancellationToken ct)
        {
            try
            {
                var updated = await _mediator.Send(new UpdateCarModelCommand(id, dto), ct);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Soft delete a car model (sets IsDeleted to true).
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeleteCarModelCommand(id), ct);
            return success ? NoContent() : NotFound();
        }
    }
}