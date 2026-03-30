using CoffeeMachineApi.Models; 

namespace CoffeeMachineApi.Services; 

public sealed record BrewOutcome(int StatusCode, BrewCoffeeResponse? Body);