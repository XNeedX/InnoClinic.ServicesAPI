using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Profiles.Presentation.Controllers;
using Services.Application.Commands;
using Services.Application.DTOs;
using Services.Application.Models;
using Services.Application.Queries;
using Services.Domain.Models;

namespace Services.Presentation.Controllers;


[ApiController]
[Route("api/[controller]")]
public class SpecializationController : ApiController
{
    private readonly IMediator _mediator;

    public SpecializationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> CreateSpecializationAsync([FromBody] CreateSpecializationCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return HandleCreatedResult(result, "Specialization created successfully");
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> EditSpecializationAsync([FromRoute] Guid id, [FromBody] EditSpecializationDTO dto, CancellationToken cancellationToken)
    {
        var command = new EditSpecializationCommand(
            id,
            dto.Name,
            dto.Price,
            dto.Status,
            dto.Category
        );
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return HandleResult(result);

        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> EditSpecializationStatusAsync([FromRoute] Guid id, [FromBody] EditStatusDTO dto, CancellationToken cancellationToken)
    {
        var command = new EditSpecializationStatusCommand(id, dto.Status);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return HandleResult(result);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> GetSpecializationAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ViewSpecializationQuery(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpGet]
    [Authorize(Roles = "Receptionist")]
    public async Task<IActionResult> GetSpecializationListAsync([FromQuery] PageParams pageParams, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ViewSpecizalizationListQuery(pageParams), cancellationToken);
        return HandleResult(result);
    }
}
