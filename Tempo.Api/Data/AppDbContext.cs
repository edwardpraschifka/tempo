using Microsoft.EntityFrameworkCore;
using Tempo.Core;

namespace Tempo.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
}