using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

using Tempo.Api.Data;

namespace Tempo.Tests.DatabaseTests;

public class MigrationTests : IAsyncLifetime
{
    private readonly MsSqlContainer _sql = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    public Task InitializeAsync() => _sql.StartAsync();
    public Task DisposeAsync() => _sql.DisposeAsync().AsTask();

    [Fact]
    public async Task Migrations_BuildSchema_FromEmptyDatabase()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_sql.GetConnectionString())
            .Options;
        await using var db = new AppDbContext(options);

        await db.Database.MigrateAsync();

        Assert.Empty(await db.Database.GetPendingMigrationsAsync());
        Assert.Equal(0, await db.Games.CountAsync());
    }
}