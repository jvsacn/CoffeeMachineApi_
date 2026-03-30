using CoffeeMachineApi.Controllers;
using CoffeeMachineApi.Models;
using CoffeeMachineApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace CoffeeMachineApi.UnitTests;

[TestFixture]
public sealed class CoffeeControllerTests
{
    private sealed class FakeCoffeeBrewService : ICoffeeBrewService
    {
        private readonly BrewOutcome _outcome;

        public FakeCoffeeBrewService(BrewOutcome outcome) => _outcome = outcome;

        public BrewOutcome Brew() => _outcome;
    }

    [Test]
    public void BrewCoffee_WhenServiceReturns200_ReturnsOkWithBody()
    {
        var expectedBody = new BrewCoffeeResponse("Your piping hot coffee is ready", "2026-03-30T09:00:00+0900");
        var outcome = new BrewOutcome(200, expectedBody);
        var controller = new CoffeeController(new FakeCoffeeBrewService(outcome));

        var result = controller.BrewCoffee();

        result.Should().BeOfType<OkObjectResult>();
        var ok = (OkObjectResult)result;
        ok.Value.Should().BeEquivalentTo(expectedBody);
    }

    [Test]
    public void BrewCoffee_WhenServiceReturns503_ReturnsStatusCode503()
    {
        var outcome = new BrewOutcome(503, null);
        var controller = new CoffeeController(new FakeCoffeeBrewService(outcome));

        var result = controller.BrewCoffee();

        result.Should().BeOfType<StatusCodeResult>();
        var ok = (StatusCodeResult)result;
        ok.StatusCode.Should().Be(503);
    }

    [Test]
    public void BrewCoffee_WhenServiceReturns418_ReturnsStatusCode418_WithEmptyBody()
    {
        var outcome = new BrewOutcome(418, null);
        var controller = new CoffeeController(new FakeCoffeeBrewService(outcome));

        var result = controller.BrewCoffee();

        result.Should().BeOfType<StatusCodeResult>();
        var obj = (StatusCodeResult)result;
        obj.StatusCode.Should().Be(418);
    }

    [Test]
    public void BrewCoffee_WhenServiceReturnsNonStandardStatus_ReturnsThatStatusCode()
    {
        var outcome = new BrewOutcome(999, null);
        var controller = new CoffeeController(new FakeCoffeeBrewService(outcome));

        var result = controller.BrewCoffee();

        result.Should().BeOfType<StatusCodeResult>();
        var obj = (StatusCodeResult)result;
        obj.StatusCode.Should().Be(999);
    }
}