using CoffeeMachineApi.Services;
using FluentAssertions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CoffeeMachineApi.Tests;

[TestFixture]
public sealed class BrewCounterUnitTests
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

    [Test]
    public async Task IncrementAndGet_is_thread_safe()
    {
        var counter = new BrewCounter();
        var tasks = new Task[10];

        for (var t = 0; t < tasks.Length; t++)
        {
            tasks[t] = Task.Run(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    counter.IncrementAndGet();
                }
            });
        }

        await Task.WhenAll(tasks);

        counter.Current.Should().Be(10 * 1000);
    }
}