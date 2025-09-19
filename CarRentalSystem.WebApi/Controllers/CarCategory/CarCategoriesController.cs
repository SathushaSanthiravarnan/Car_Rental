using Asp.Versioning;
using CarRentalSystem.Application.Commands.CarCategory;
using CarRentalSystem.Application.DTOs.CarCategory;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.Queries.CarCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.CarCategory
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class CarCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarCategoriesController(IMediator mediator) => _mediator = mediator;

        /// <summary>Create a new car category.</summary>//
        [HttpPost]
        public async Task<ActionResult<CarCategoryDto>> Create([FromBody] CreateCarCategoryDto dto, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreateCarCategoryCommand(dto), ct);
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
            return CreatedAtAction(nameof(GetById), routeValues: new { version, id = created.Id }, value: created);
        }

        /// <summary>Get a single car category by Id.</summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CarCategoryDto?>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetCarCategoryByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Get paged car categories.</summary>//
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<CarCategoryDto>>> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 1;
            if (pageSize > 200) pageSize = 200;

            var result = await _mediator.Send(new GetCarCategoriesPagedQuery(page, pageSize), ct);
            return Ok(result);
        }

        /// <summary>Update an existing car category.</summary>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CarCategoryDto>> Update(Guid id, [FromBody] UpdateCarCategoryDto dto, CancellationToken ct)
        {
            var updated = await _mediator.Send(new UpdateCarCategoryCommand(id, dto), ct);
            return Ok(updated);
        }

        /// <summary>Soft delete a car category.</summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeleteCarCategoryCommand(id), ct);
            return success ? NoContent() : NotFound();
        }
    }
}






