using CoffeeMachineApi.Services;
using FluentAssertions;
using NUnit.Framework;

namespace CoffeeMachineApi.UnitTests;

public sealed class BrewCounterTests
{
    [Test]
    public void IncrementAndGet_increments_and_Current_reflects_value()
    {
        var counter = new BrewCounter();

        counter.IncrementAndGet().Should().Be(1);
        counter.IncrementAndGet().Should().Be(2);
        counter.IncrementAndGet().Should().Be(3);

        counter.Current.Should().Be(3);
    }
}