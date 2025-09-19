using Asp.Versioning;
using CarRentalSystem.Application.Commands.TestDrive;
using CarRentalSystem.Application.DTOs.Common;
using CarRentalSystem.Application.DTOs.TestDrive;
using CarRentalSystem.Application.Queries.TestDrive;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.TestDrive
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces("application/json")]
    public class TestDrivesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestDrivesController(IMediator mediator) => _mediator = mediator;

        /// <summary>
        /// Request a new test drive.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TestDriveDto>> Create([FromBody] CreateTestDriveDto dto, CancellationToken ct)
        {
            var created = await _mediator.Send(new CreateTestDriveCommand(dto), ct);
            return CreatedAtAction("GetById", new { id = created.Id }, created);
        }

        /// <summary>
        /// Update the status of a test drive (e.g., Approve, Reject).
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateTestDriveDto dto, CancellationToken ct)
        {
            if (id != dto.Id)
            {
                return BadRequest("ID in URL and body do not match.");
            }

            var success = await _mediator.Send(new UpdateTestDriveCommand(dto), ct);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Soft deletes a test drive by its ID.
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
        {
            var success = await _mediator.Send(new SoftDeleteTestDriveCommand(id), ct);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Get a single test drive by its ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TestDriveDto>> GetById(Guid id, CancellationToken ct)
        {
            var testDrive = await _mediator.Send(new GetTestDriveByIdQuery(id), ct);
            return testDrive is null ? NotFound() : Ok(testDrive);
        }

        /// <summary>
        /// Get a paged list of all test drives.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedResultDto<TestDriveDto>>> GetPaged(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _mediator.Send(new GetTestDrivesPagedQuery(page, pageSize), ct);
            return Ok(result);
        }
    }
}
