using Asp.Versioning;
using CarRentalSystem.Application.Commands.Booking;
using CarRentalSystem.Application.DTOs.Booking;
using CarRentalSystem.Application.Queries.Booking;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.BookingsController
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator) => _mediator = mediator;

        /// <summary>Creates a new booking.</summary>
        /// <response code="201">Returns the newly created booking.</response>
        /// <response code="404">If the car or customer is not found.</response>
        /// <response code="409">If the car is already booked or dates are invalid.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<BookingDto>> Create([FromBody] CreateBookingDto dto, CancellationToken ct)
        {
            try
            {
                var created = await _mediator.Send(new CreateBookingCommand(dto), ct);
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

        /// <summary>Get a single booking by Id.</summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookingDto?>> GetById(Guid id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetBookingByIdQuery(id), ct);
            return result is null ? NotFound() : Ok(result);
        }

        /// <summary>Update an existing booking.</summary>
        /// <response code="200">Returns the updated booking.</response>
        /// <response code="404">If the booking is not found.</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookingDto>> Update(Guid id, [FromBody] UpdateBookingDto dto, CancellationToken ct)
        {
            try
            {
                var updated = await _mediator.Send(new UpdateBookingCommand(id, dto), ct);
                return Ok(updated);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>Soft delete a booking (marks IsDeleted = true).</summary>
        /// <response code="204">The booking was successfully soft-deleted.</response>
        /// <response code="404">If the booking is not found.</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeleteBookingCommand(id), ct);
            return success ? NoContent() : NotFound();
        }
    }
}
