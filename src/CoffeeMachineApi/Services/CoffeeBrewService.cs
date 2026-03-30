
using System.Globalization;
using CoffeeMachineApi.Models;

namespace CoffeeMachineApi.Services;

public sealed class CoffeeBrewService : ICoffeeBrewService
{
    private readonly BrewCounter _counter;
    private readonly IDateTimeProvider _clock;
    private const string Message = "Your piping hot coffee is ready";

    public CoffeeBrewService(BrewCounter counter, IDateTimeProvider clock)
    {
        _counter = counter;
        _clock = clock;
    }

    public BrewOutcome Brew()
    {
        var now = _clock.Now;
        if (now.Month == 4 && now.Day == 1)
            return new BrewOutcome(418, null);

        var call = _counter.IncrementAndGet();
        if (call % 5 == 0)
            return new BrewOutcome(503, null);

        var s = now.ToString("yyyy-MM-dd'T'HH:mm:sszzz", CultureInfo.InvariantCulture);
        s = s.Remove(s.Length - 3, 1);

        return new BrewOutcome(200, new BrewCoffeeResponse(Message, s));
    }
}
