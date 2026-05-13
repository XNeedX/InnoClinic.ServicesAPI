using Services.Domain.Models;

namespace Services.Application.DTOs;

public sealed record EditSpecializationDTO(string Name, decimal Price, Status Status, Category Category)
{
};
