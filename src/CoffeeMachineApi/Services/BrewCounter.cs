
using System.Threading;
namespace CoffeeMachineApi.Services;
public sealed class BrewCounter
{
    private long _count;

    // Atomically increment the counter and return the new value
    public long IncrementAndGet()
    {
        return Interlocked.Increment(ref _count);
    }

    // Expose the current counter value (atomic read)
    public long Current => Interlocked.Read(ref _count);
}
