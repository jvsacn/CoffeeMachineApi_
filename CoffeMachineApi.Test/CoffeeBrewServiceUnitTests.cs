using CoffeeMachineApi.Services;
using CoffeeMachineApi.UnitTests;
using FluentAssertions;
using NUnit.Framework;
using System;

namespace CoffeeMachineApi.Tests;

[TestFixture]
public sealed class CoffeeBrewServiceUnitTests
{
    [Test]
    public void Brew_returns_503_on_5th_call_and_200_otherwise()
    {
        var clock = new FixedDateTimeProvider(new DateTimeOffset(2021, 2, 3, 11, 56, 24, TimeSpan.FromHours(9)));
        var counter = new BrewCounter();
        var svc = new CoffeeBrewService(counter, clock);

        for (var i = 1; i <= 4; i++)
        {
            var ok = svc.Brew();
            ok.StatusCode.Should().Be(200);
            ok.Body.Should().NotBeNull();
        }

        var fifth = svc.Brew();
        fifth.StatusCode.Should().Be(503);
        fifth.Body.Should().BeNull();

        var sixth = svc.Brew();
        sixth.StatusCode.Should().Be(200);
        sixth.Body.Should().NotBeNull();
    }

    [Test]
    public void Brew_returns_418_on_april_first_without_incrementing_counter()
    {
        var april1 = new FixedDateTimeProvider(new DateTimeOffset(2026, 4, 1, 8, 0, 0, TimeSpan.FromHours(8)));
        var counter = new BrewCounter();
        var svc = new CoffeeBrewService(counter, april1);

        var res1 = svc.Brew();
        res1.StatusCode.Should().Be(418);
        counter.Current.Should().Be(0);

        var res2 = svc.Brew();
        res2.StatusCode.Should().Be(418);
        counter.Current.Should().Be(0);
    }
}