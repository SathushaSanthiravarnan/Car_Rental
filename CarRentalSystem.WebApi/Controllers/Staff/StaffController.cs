// WebApi/Controllers/Staff/StaffController.cs
using Asp.Versioning;
using CarRentalSystem.Application.Commands.Staff;
using CarRentalSystem.Application.DTOs.Staff;
using CarRentalSystem.Application.Queries.Staff;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.WebApi.Controllers.Staff;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public sealed class StaffController : ControllerBase
{
    private readonly IMediator _mediator;
    public StaffController(IMediator mediator) => _mediator = mediator;

    // ------------------------------------------
    // Helpers
    // ------------------------------------------
    private string Caller() =>
        User?.Identity?.IsAuthenticated == true
            ? (User.Identity!.Name ?? "user")
            : "system";

    // Tiny request types for body-only inputs
    public sealed record ExtendInviteExpiryRequest(DateTime NewExpiryUtc);
    public sealed record ResendInviteRequest(string? Email);

    // ------------------------------------------
    // CRUD: Staff
    // ------------------------------------------

    /// <summary>Get a single staff by id.</summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StaffDto?>> GetById(Guid id, CancellationToken ct)
    {
        var item = await _mediator.Send(new GetStaffByIdQuery(id), ct);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>Get paged staff list.</summary>
    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 20, CancellationToken ct = default)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 1;
        if (pageSize > 200) pageSize = 200;

        var result = await _mediator.Send(new GetStaffPagedQuery(page, pageSize), ct);
        return Ok(result);
    }

    /// <summary>Create staff (links to an existing UserId).</summary>
    [HttpPost]
    public async Task<ActionResult<StaffDto>> Create([FromBody] CreateStaffDto dto, CancellationToken ct)
    {
        var created = await _mediator.Send(new CreateStaffCommand(dto), ct);
        var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
        return CreatedAtAction(nameof(GetById), new { version, id = created.Id }, created);
    }

    /// <summary>Update staff by id.</summary>
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<StaffDto>> Update(Guid id, [FromBody] UpdateStaffDto dto, CancellationToken ct)
    {
        var updated = await _mediator.Send(new UpdateStaffCommand(id, dto), ct);
        return Ok(updated);
    }

    /// <summary>Soft-delete staff by id.</summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
    {
        var ok = await _mediator.Send(new SoftDeleteStaffCommand(id), ct);
        return ok ? NoContent() : NotFound();
    }

    // ------------------------------------------
    // Invitations
    // ------------------------------------------

    /// <summary>Create a staff invite (sends an email with a tokenized link).</summary>
    [HttpPost("invites")]
    public async Task<ActionResult<StaffInviteDto>> CreateInvite([FromBody] CreateStaffInviteDto dto, CancellationToken ct)
    {
        var result = await _mediator.Send(new CreateStaffInviteCommand(dto), ct);
        return Ok(result);
    }

    /// <summary>Accept an invite using the token and basic details.</summary>
    [HttpPost("invites/accept")]
    public async Task<ActionResult<StaffDto>> AcceptInvite(
    [FromBody] AcceptStaffInviteDto dto,
    CancellationToken ct)
    {
        var created = await _mediator.Send(new AcceptStaffInviteCommand(dto), ct);
        if (created is null) return BadRequest("Invalid or expired invite");

        var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
        return CreatedAtAction(nameof(GetById), new { version, id = created.Id }, created);
    }

    /// <summary>Refresh (rotate) a token for an existing invite.</summary>
    [HttpPost("invites/{id:guid}/refresh-token")]
    public async Task<ActionResult<StaffInviteDto>> RefreshInviteToken(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(
            new RefreshStaffInviteTokenCommand(id, RequestedBy: Caller()),
            ct);

        return Ok(result);
    }

    /// <summary>Extend the expiry of an existing invite.</summary>
    [HttpPost("invites/{id:guid}/extend-expiry")]
    public async Task<ActionResult<StaffInviteDto>> ExtendInviteExpiry(
        Guid id,
        [FromBody] ExtendInviteExpiryRequest body,
        CancellationToken ct)
    {
        var result = await _mediator.Send(
            new ExtendStaffInviteExpiryCommand(
                InviteId: id,
                NewExpiryUtc: body.NewExpiryUtc,
                RequestedBy: Caller()),
            ct);

        return Ok(result);
    }

    /// <summary>Re-send an invite email (optionally override destination email).</summary>
    [HttpPost("invites/{id:guid}/resend")]
    public async Task<IActionResult> ResendInvite(
        Guid id,
        [FromBody] ResendInviteRequest body,
        CancellationToken ct)
    {
        var ok = await _mediator.Send(
            new ResendStaffInviteCommand(
                InviteId: id,
                Email: body.Email,          // if null, handler can use the invite's current email
                RequestedBy: Caller()),
            ct);

        return ok ? Ok() : BadRequest();
    }
}