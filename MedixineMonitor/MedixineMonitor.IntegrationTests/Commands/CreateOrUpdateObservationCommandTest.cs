using MedixineMonitor.Application.Observations.Commands;
using NUnit.Framework;
using FluentAssertions;
using MedixineMonitor.Domain.Enums;

namespace MedixineMonitor.IntegrationTests.Commands;

using static Testing;

internal class CreateOrUpdateObservationCommandTest : TestBase
{
    [Test]
    public async Task ShouldCreateAndUpdateObservationItem()
    {
        var command = new CreateOrUpdateObservationCommand
        {
            Name = "observationCreatingIntegrationTESTS_1",
            Type = HealthMetrics.BloodPressure,
            Value = 100,
            Description = "test test test"
        };

        await SendAsync(command);

        var item = await FindObservationAsync(command.Name);

        item.Should().NotBeNull();
        item.Name.Should().Be(command.Name);
        item.Type.Should().Be(command.Type);
        item.Value.Should().Be(command.Value);
        item.Description.Should().Be(command.Description);

        command.Id = item.Id;
        command.Name = "observationUpdatingIntegrationTESTS_2";
        command.Type = HealthMetrics.Pulse;
        command.Value = 101;
        command.Description = "updates test";

        await SendAsync(command);

        var item1 = await FindObservationAsync(command.Name);

        item1.Should().NotBeNull();
        item1.Id.Should().Be(command.Id.Value);
        item1.Name.Should().Be(command.Name);
        item1.Type.Should().Be(command.Type);
        item1.Value.Should().Be(command.Value);
        item1.Description.Should().Be(command.Description);
    }
}

