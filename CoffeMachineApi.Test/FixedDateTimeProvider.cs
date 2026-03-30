using System;
using CoffeeMachineApi.Services;

namespace CoffeeMachineApi.UnitTests;

public sealed class FixedDateTimeProvider : IDateTimeProvider
{
    public FixedDateTimeProvider(DateTimeOffset now) => Now = now;
    public DateTimeOffset Now { get; }
}