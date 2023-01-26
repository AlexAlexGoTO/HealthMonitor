using MedixineMonitor.Application.Observations.Commands;
using MedixineMonitor.Application.Observations.Queries;
using NUnit.Framework;
using FluentAssertions;
using MedixineMonitor.Domain.Enums;

namespace MedixineMonitor.IntegrationTests.Queries;

using static Testing;

internal class GetObservationQueryTest : TestBase
{
    [Test]
    public async Task ShouldReturnObservation()
    {
        var command = new CreateOrUpdateObservationCommand
        {
            Name = "observationCreatingIntegrationTESTS_2",
            PatientId = 1,
            Type = HealthMetrics.BloodOxygenSaturation,
            Value = 100,
            Description = "read test" 
        };

        await SendAsync(command);

        var item = await FindObservationAsync(command.Name);

        var query = new GetObservationQuery(item.Id);

        var result = await SendAsync(query);

        result.Should().NotBeNull();
        result.Id!.Value.Should().Be(item.Id);
        result.Name.Should().Be(command.Name);
        result.Type.Should().Be(command.Type);
        result.Value.Should().Be(command.Value);
        result.Description.Should().Be(command.Description);
    }
}

