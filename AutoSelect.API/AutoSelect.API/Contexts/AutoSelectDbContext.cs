namespace AutoSelect.API.Contexts;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;

/// <summary>
/// Контекст бази данних.
/// </summary>
public class AutoSelectDbContext(DbContextOptions<AutoSelectDbContext> options)
    : IdentityDbContext<User>(options)
{
    /// <inheritdoc />
    public required DbSet<Expert> Experts { get; set; }

    /// <inheritdoc />
    public required DbSet<Client>? Clients { get; set; }
}
