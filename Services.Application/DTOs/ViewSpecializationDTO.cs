using Services.Domain.Models;

namespace Services.Application.DTOs;

public sealed record ViewSpecializationDTO
(
    Guid Id,
    Status Status,
    string Name,
    List<ViewSpecializationServiceDTO> Services
);

public sealed record ViewSpecializationServiceDTO
(
    Guid Id,
    string Name,
    decimal Price,
    Status Status,
    Category Category
);