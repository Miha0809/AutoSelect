using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using AutoSelect.API.Models.Client;
using AutoSelect.API.Models.Expert;
using AutoSelect.API.Models.User;

namespace AutoSelect.API.Context;

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

    /// <inheritdoc />
    public required DbSet<ServiceInfo> ServiceInfos { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder
            .Entity<Expert>()
            .ToTable("Experts")
            .HasBaseType<User>();
    
        modelBuilder
            .Entity<Client>()
            .ToTable("Clients")
            .HasBaseType<User>();
    }
}
