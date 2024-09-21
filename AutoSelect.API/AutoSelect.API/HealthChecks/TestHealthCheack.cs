using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AutoSelect.API.HealthChecks;

/// <summary>
/// [TODO:description]
/// </summary>
public class TestHealthCheack : IHealthCheck
{
    /// <summary>
    /// [TODO:description]
    /// </summary>
    /// <param name="context">[TODO:description]</param>
    /// <param name="cancellationToken">[TODO:description]</param>
    /// <returns>[TODO:description]</returns>
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default
    )
    {
        var isHealthy = await IsDatabaseConnectionOkAsync();

        return isHealthy ? HealthCheckResult.Healthy("OK") : HealthCheckResult.Unhealthy("ERROR");
    }

    private Task<bool> IsDatabaseConnectionOkAsync()
    {
        return Task.FromResult(true);
    }
}
