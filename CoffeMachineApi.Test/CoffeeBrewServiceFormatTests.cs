using CoffeeMachineApi.Services;
using CoffeeMachineApi.UnitTests;
using FluentAssertions;

namespace CoffeeMachineApi.Tests;

[TestFixture]
public sealed class CoffeeBrewServiceFormatTests
{
    [Test]
    public void Brew_formats_prepared_time_without_colon_in_offset()
    {
        var now = new DateTimeOffset(2021, 2, 3, 11, 56, 24, TimeSpan.FromHours(9));
        var clock = new FixedDateTimeProvider(now);
        var counter = new BrewCounter();
        var svc = new CoffeeBrewService(counter, clock);

        var outcome = svc.Brew();

        outcome.StatusCode.Should().Be(200);
        outcome.Body.Should().NotBeNull();
        outcome.Body!.Prepared.Should().Be("2021-02-03T11:56:24+0900");
    }
}