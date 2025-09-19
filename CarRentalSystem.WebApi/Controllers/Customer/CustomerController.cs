using Asp.Versioning;
using CarRentalSystem.Application.Commands.Customers;
using CarRentalSystem.Application.DTOs.Customer;
using CarRentalSystem.Application.Queries.Customers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.Customer
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {
     private readonly IMediator _mediator;

        public CustomerController(IMediator mediator) => _mediator = mediator;

    /// <summary>Create a new customer profile for a user.</summary>
    /// <response code="201">Returns the newly created customer.</response>
    /// <response code="404">If the user is not found.</response>
    /// <response code="409">If the user already has a customer profile.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto, CancellationToken ct)
    {
        try
        {
            var created = await _mediator.Send(new CreateCustomerCommand(dto), ct);
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(
                nameof(GetById),
                routeValues: new { version, id = created.Id },
                value: created
            );
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>Get a single customer by Id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerDto?>> GetById(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCustomerByIdQuery(id), ct);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CustomerDto>> Update(Guid id, [FromBody] UpdateCustomerDto dto, CancellationToken ct)
    {
        try
        {
            var updated = await _mediator.Send(new UpdateCustomerCommand(id, dto), ct);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>Soft delete a customer (marks IsDeleted = true).</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
    {
        var success = await _mediator.Send(new SoftDeleteCustomerCommand(id), ct);
        return success ? NoContent() : NotFound();
    }
        }
}
