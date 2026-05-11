namespace Services.Application.Results;
public static class ServiceErrors
{
    public static readonly Error ServiceNotFound = new(
        "Service.NotFound",
        "The service was not found.",
        ErrorType.NotFound
    );
}