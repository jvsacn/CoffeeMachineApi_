
using CoffeeMachineApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachineApi.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class CoffeeController : ControllerBase
{
    private readonly ICoffeeBrewService _service;

    public CoffeeController(ICoffeeBrewService service)
    {
        _service = service;
    }

    /// <summary>
    /// Brews a cup of coffee.
    /// </summary>
    /// <remarks>
    /// - Returns 200 with coffee details
    /// - Every 5th call returns 503 (out of coffee)
    /// - April 1st always returns 418 (I'm a teapot)
    /// </remarks>
    [HttpGet("/brew-coffee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status418ImATeapot)]
    public IActionResult BrewCoffee()
    {
        var outcome = _service.Brew();
        return outcome.StatusCode switch
        {
            200 => Ok(outcome.Body),
            503 => StatusCode(503),
            418 => StatusCode(418),
            _ => StatusCode(outcome.StatusCode)
        };
    }
}
