using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Tempo.Tests;

public class HealthTests
{
    [Fact]
    public async Task Health_ReturnsOk()
    {
        await using var factory = new WebApplicationFactory<Program>();
        var client = factory.CreateClient();
        var response = await client.GetAsync("/health");
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
