# Tempo

A chess training site where you play a human-like bot at a chosen rating, then get an engine review that explains your mistakes.

**Status: early development.** The project skeleton is in place: an ASP.NET Core API with a health endpoint, a SQL Server database built from EF Core migrations, and tests that run on every push. Game analysis, play against the bot and the front end are not built yet.

## Planned features

- **Play** against a human-like bot (Maia) at a rating from 500 to 2000.
- **Review** each game with Stockfish: an evaluation graph, marked mistakes and an accuracy score.
- **Understand** each mistake through a plain-language explanation.
- **Track** results by opening and the kinds of mistake you make most.

## Tech stack

| Layer | Technology | Status |
| --- | --- | --- |
| API | C#, ASP.NET Core, REST | In progress |
| Database | SQL Server with Entity Framework Core | In progress |
| Tests | xUnit, Testcontainers | In progress |
| CI | GitHub Actions | In progress |
| Front end | TypeScript and React | Planned |
| Engines | Stockfish, and Maia served by a Python service | Planned |
| Hosting | Docker, Azure Container Apps, Azure SQL | Planned |

## Project structure

```
tempo/
├── Tempo.slnx               Solution file listing the projects
├── Tempo.Api/               ASP.NET Core API: controllers, database context, migrations
├── Tempo.Core/              Domain classes, with no web or database code
├── Tempo.Tests/             xUnit tests
├── docker-compose.yml       Runs SQL Server locally
├── run.sh                   Starts the database and the API
└── .github/workflows/       CI pipeline
```

## Running locally

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://docs.docker.com/get-docker/)
- The EF Core command-line tool: `dotnet tool install --global dotnet-ef`

### 1. Set the database password

Create a `.env` file in the repository root:

```
DB_PASSWORD=<a strong password>
```

SQL Server requires at least eight characters, including upper case, lower case, a digit and a symbol. This file is ignored by Git.

### 2. Give the API its connection string

The connection string is stored in .NET user secrets, outside the repository. Use the same password as in `.env`:

```bash
dotnet user-secrets init --project Tempo.Api
dotnet user-secrets set "ConnectionStrings:Default" 'Server=localhost,1433;Database=Tempo;User Id=sa;Password=<your password>;TrustServerCertificate=True' --project Tempo.Api
```

### 3. Create the database

Start SQL Server, then apply the migrations:

```bash
docker compose up -d
dotnet ef database update --project Tempo.Api
```

### 4. Run the app

```bash
./run.sh
```

If you get "permission denied", run `bash run.sh` instead.

This starts SQL Server in the background and the API in the foreground. Stop the API with Ctrl+C, and the database with `docker compose down`.

Check it's running by requesting the health endpoint, using the address shown in the terminal:

```bash
curl http://localhost:<port>/health
```

## Running tests

```bash
dotnet test
```

Docker must be running: the migration test starts its own temporary SQL Server container, applies every migration to it, and deletes it afterwards.

## Continuous integration

GitHub Actions builds the solution and runs every test on each push and pull request. The workflow is in `.github/workflows/ci.yml`.
