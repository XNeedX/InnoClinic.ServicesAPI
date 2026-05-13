using Services.Domain.Models;

namespace Services.Application.DTOs;

public sealed record EditServiceDTO(string Name, decimal Price, Category Category, Status Status)
{
}
