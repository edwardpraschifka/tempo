# tempo

ASP.NET Core Web API (.NET 10) backed by SQL Server via EF Core.

## Projects

- `Tempo.Api` – web API, `AppDbContext`, and EF Core migrations
- `Tempo.Core` – domain entities
- `Tempo.Tests` – xUnit tests (uses Testcontainers for SQL Server)

## Prerequisites

- .NET 10 SDK
- Docker

## Setup

1. Create a `.env` file in the repo root with the SQL Server SA password:

   ```
   DB_PASSWORD=<your-strong-password>
   ```

2. Set the API connection string with user secrets:

   ```
   dotnet user-secrets set "ConnectionStrings:Default" \
     "Server=localhost,1433;Database=Tempo;User Id=sa;Password=<your-strong-password>;TrustServerCertificate=True" \
     --project Tempo.Api
   ```

3. Apply migrations (requires `dotnet-ef`):

   ```
   docker compose up -d
   dotnet ef database update --project Tempo.Api
   ```

## Running

```
./run.sh
```

This starts the SQL Server container and runs the API at `http://localhost:5091`. Check it with:

```
curl http://localhost:5091/health
```

## Tests

```
dotnet test
```

Tests start their own SQL Server container, so Docker must be running.
