using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Services.Application.Handlers;
using System.Reflection;

namespace Services.Application.Extensions;

public static class DependencyInjection
{
    public static ContainerBuilder AddApplicationLayer(this ContainerBuilder builder)
    {
        //var assembly = typeof(EditStatusHandler<>).GetTypeInfo().Assembly;

        //var configuration = MediatRConfigurationBuilder
        //    .Create("eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODA5OTkzNjAwIiwiaWF0IjoiMTc3ODQ5ODQyMiIsImFjY291bnRfaWQiOiIwMTllMTZjMzZkMWM3MTBlYjJjOTc3NWVkNWUxN2ExNSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa3JiYzhoenh2cHhlejltcDlxMHd6ZWh6Iiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.ivEkbSKiW7xtMT8w5bWhfAkDzxD6N4eUWI4DDpv-2VT-NllZlNYnHxdO26etLe6jmPRfy2GysHHTm6Guhiuu72h2GltJx3MT4W0nMkbZOrWGFkGmArdC6AfuC2USY1RIvwZ3-oXnQNON-2FklW0znj3t51BugI0WnR5bOkNehcq5faq4sswGppc3WRLxEwwLi7_2YKeNJvWaNoIFMc2-N5sfSKnztDTsiC3QsrbCqZ9nA0aYIy0bGLJBMbHb3jFvVKHvuca3R9-iJIQqztEn1D0IQ_uRf2JQSnQQuXSLKVwG5xC33vOE53A6iby7Bslpdmq9ICoGLwXqzNwp4rxE_Q", assembly)
        //    .Build();

        //builder.RegisterMediatR(configuration);

        builder.RegisterGeneric(typeof(EditStatusHandler<>))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope(); 

        return builder;
    }
}
