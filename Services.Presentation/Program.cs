using Autofac;
using Autofac.Extensions.DependencyInjection;
using Services.Application.Extensions;
using Services.Infrastructure.Extensions;
using Services.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Services.Application.Extensions.DependencyInjection).Assembly));
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.AddApplicationLayer();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ConfigureMiddlewarePipeline();

app.Run();
