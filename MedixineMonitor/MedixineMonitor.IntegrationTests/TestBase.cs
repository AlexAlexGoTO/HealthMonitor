using NUnit.Framework;

namespace MedixineMonitor.IntegrationTests;

using static Testing;
public class TestBase
{
    [SetUp]
    public async Task TestSetUp()
    {
        await ResetState();
    }
}
