using Services.Domain.Models;

namespace Services.Application.DTOs;

public sealed record ViewSpecializationListDTO
(
    Guid Id,
    string Name,
    Status Status
);
