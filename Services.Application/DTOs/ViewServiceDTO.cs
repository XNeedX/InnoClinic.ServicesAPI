
using Services.Domain.Models;

namespace Services.Application.DTOs;

public sealed record ViewServiceDTO
(
    Guid Id,
    string Name,
    decimal Price,
    Category Category,
    Status Status
);
