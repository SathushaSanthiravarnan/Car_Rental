using Asp.Versioning;
using CarRentalSystem.Application.Commands.Payment;
using CarRentalSystem.Application.DTOs.Payment;
using CarRentalSystem.Application.Queries.Payment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.Payment
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator) => _mediator = mediator;

        /// <summary>Get a single payment by Id.</summary>
        /// <response code="200">Returns the requested payment.</response>
        /// <response code="404">If the payment is not found.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentDto>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetPaymentByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Creates a new payment for a booking.</summary>
        /// <response code="201">Returns the newly created payment.</response>
        /// <response code="400">If the payment data is invalid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentDto>> Create([FromBody] CreatePaymentDto dto, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreatePaymentCommand(dto), ct);
            var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

            return CreatedAtAction(
                nameof(GetById),
                routeValues: new { version, id = created.Id },
                value: created
            );
        }

        /// <summary>Update an existing payment.</summary>
        /// <response code="200">Returns the updated payment.</response>
        /// <response code="404">If the payment is not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentDto>> Update(Guid id, [FromBody] UpdatePaymentDto dto, CancellationToken ct)
        {
            var updated = await _mediator.Send(new UpdatePaymentCommand(id, dto), ct);
            return Ok(updated);
        }

        /// <summary>Soft deletes a payment (marks as IsDeleted).</summary>
        /// <response code="204">If the payment was successfully deleted.</response>
        /// <response code="404">If the payment is not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeletePaymentCommand(id), ct);
            return success ? NoContent() : NotFound();
        }

    }
}
