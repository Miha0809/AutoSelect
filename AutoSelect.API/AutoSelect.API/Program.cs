using System.Reflection;
using AutoSelect.API.Contexts;
using AutoSelect.API.Models;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Profiles;
using AutoSelect.API.Repositories;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services;
using AutoSelect.API.Services.Interfaces;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "oauth2",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
        }
    );
    options.OperationFilter<SecurityRequirementsOperationFilter>();
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "AutoSelect",
            Version = "v1",
            Description = "Description",
        }
    );

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("Host")!;
builder.Services.AddDbContext<AutoSelectDbContext>(options =>
{
    options.UseLazyLoadingProxies().UseNpgsql(connectionString);
});
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AutoSelectDbContext>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// DI for repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserSearchRepository, UserSearchRepository>();

// DI for services
builder.Services.AddScoped<IProfileService, ProfileService>();

// Health Checks
builder
    .Services.AddHealthChecks()
    .AddNpgSql(
        connectionString: connectionString,
        healthQuery: "SELECT 1",
        name: "NpgSql Check",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "sql" }
    );

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    }
);
app.MapHealthChecksUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints => { endpoints?.MapControllers(); });

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { nameof(Roles.Client), nameof(Roles.Expert) };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
        else
        {
        }
    }
}

app.Run();