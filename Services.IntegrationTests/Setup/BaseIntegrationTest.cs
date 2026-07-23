using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Infrastructure.Data;
using Services.IntegrationTests.Setup;
using System.Net.Http.Headers;
using Xunit;

public abstract class BaseIntegrationTest : IClassFixture<ApiWebApplicationFactory>, IAsyncDisposable
{
    protected readonly HttpClient Client;
    protected readonly ApiWebApplicationFactory Factory;
    private readonly IServiceScope _scope;
    protected readonly ServicesDbContext DbContext; 

    protected BaseIntegrationTest(ApiWebApplicationFactory factory)
    {
        Factory = factory;
        Client = factory.CreateClient();

        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(TestAuthHandler.AuthenticationScheme);

        _scope = factory.Services.CreateScope();
        DbContext = _scope.ServiceProvider.GetRequiredService<ServicesDbContext>();

        DbContext.Database.Migrate();
    }

    public async ValueTask DisposeAsync()
    {
        DbContext.Specializations.RemoveRange(DbContext.Specializations);
        DbContext.Services.RemoveRange(DbContext.Services );

        await DbContext.SaveChangesAsync();
        _scope.Dispose();
    }
}