# Developer Technical Test – Coffee Machine API (.NET)

This repository contains an ASP.NET Core HTTP API that controls an imaginary internet-connected coffee machine.

## Requirements implemented

- `GET /brew-coffee`
  - Returns **200 OK** with JSON body:

    ```json
    {
      "message": "Your piping hot coffee is ready",
      "prepared": "2021-02-03T11:56:24+0900"
    }
    ```

  - On every **5th call**, returns **503 Service Unavailable** with an **empty body**.
  - On **April 1st**, returns **418 I'm a teapot** with an **empty body** (overrides the 5th call rule).

## Running

```bash
dotnet restore

dotnet test

dotnet run --project src/CoffeeMachineApi
```

Then browse:

- http://localhost:5000/brew-coffee (or the port shown in the console)

## Notes

- The `prepared` timestamp uses ISO-8601 datetime with a "basic" UTC offset (e.g., `+0900`) to match the sample in the prompt.
