using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Services.Application.Commands;
using Services.Domain.Models;
using Services.IntegrationTests.Setup;
using Xunit;

namespace Services.IntegrationTests.Controllers;

public class SpecializationControllerTests : BaseIntegrationTest
{
    public SpecializationControllerTests(ApiWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateSpecialization_ShouldReturnCreated_WhenDataIsValid()
    {
        var command = new CreateSpecializationCommand("Cardiology", 1500m, Status.Active, Category.Consultations);

        var response = await Client.PostAsJsonAsync("/api/specialization", command);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var dbSpecialization = DbContext.Specializations.FirstOrDefault(s => s.Name == "Cardiology");
        dbSpecialization.Should().NotBeNull();
        dbSpecialization!.Price.Should().Be(1500m);
    }

    [Fact]
    public async Task GetSpecialization_ShouldReturnSpecialization_WhenExists()
    {
        var specialization = new Specialization
        {
            Name = "Neurology",
            Price = 2000m,
            Status = Status.Active,
            Category = Category.Consultations
        };
        DbContext.Specializations.Add(specialization);
        await DbContext.SaveChangesAsync();

        var response = await Client.GetAsync($"/api/specialization/{specialization.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task EditSpecializationStatus_ShouldReturnNoContent_WhenValid()
    {
        var specialization = new Specialization
        {
            Name = "Therapy",
            Price = 1000m,
            Status = Status.Active,
            Category = Category.Diagnostics
        };
        DbContext.Specializations.Add(specialization);
        await DbContext.SaveChangesAsync();

        var editCommand = new { status = Status.Inactive };

        var response = await Client.PatchAsJsonAsync($"/api/specialization/{specialization.Id}/status", editCommand);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var updatedSpecialization = await DbContext.Specializations.FindAsync(specialization.Id);
        updatedSpecialization!.Status.Should().Be(Status.Inactive);
    }
}