using Autofac;

namespace Services.Application.Extensions;

public static class DependencyInjection
{
    public static ContainerBuilder AddApplicationLayer(this ContainerBuilder builder)
    {
        return builder;
    }
}
