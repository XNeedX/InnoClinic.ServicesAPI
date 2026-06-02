namespace Services.Application.Models;

public record PagedResult<T>(
    IReadOnlyCollection<T> Items,
    int TotalCount
);