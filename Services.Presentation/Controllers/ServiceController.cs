using MediatR;
using Microsoft.AspNetCore.Mvc;
using Profiles.Presentation.Controllers;
using Services.Application.Commands;
using Services.Application.Queries;
using Services.Application.DTOs;
using Services.Domain.Models;

namespace Services.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServiceController : ApiController
{
    private readonly IMediator _mediator;

    public ServiceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateServiceAsync([FromBody] CreateServiceCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);

        return HandleCreatedResult(result, "Service created successfully");
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetServiceByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ViewServiceQuery(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> EditServiceAsync([FromRoute]Guid id, [FromBody] EditServiceDTO dto, CancellationToken cancellationToken) 
    { 
        var command = new EditServiceCommand(
            id,
            dto.Name,
            dto.Status,
            dto.Price,
            dto.Category
        );
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess) 
            return HandleResult(result);

        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> EditServiceStatusAsync([FromRoute] Guid id, [FromBody] EditStatusDTO dto, CancellationToken cancellationToken)
    {
        var command = new EditServiceStatusCommand(id, dto.Status);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return HandleResult(result);

        return NoContent();
    }

    [HttpGet("by-category/{category}")]
    public async Task<IActionResult> GetServicesByCategoryAsync([FromRoute] Category category, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ViewServiceByCategoryQuery(category), cancellationToken);
        return HandleResult(result);
    }
}