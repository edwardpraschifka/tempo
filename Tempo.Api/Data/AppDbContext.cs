using Microsoft.EntityFrameworkCore;
using Tempo.Core;

namespace Tempo.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Move> Moves => Set<Move>();
    public DbSet<Evaluation> Evaluations => Set<Evaluation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Move>(move =>
        {
            move.HasIndex(m => new { m.GameId, m.MoveNumber }).IsUnique();
        });
    }
}