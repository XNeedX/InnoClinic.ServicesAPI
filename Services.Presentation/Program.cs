using Microsoft.AspNetCore.Mvc;
using Services.Application.Commands;
using Services.Infrastructure.Extensions;
using Services.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateServiceCommand).Assembly));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ConfigureMiddlewarePipeline();

app.Run();
