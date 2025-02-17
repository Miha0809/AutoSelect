using System.Reflection;
using AutoSelect.API.Context;
using AutoSelect.API.Models.User;
using AutoSelect.API.Models.Enums;
using AutoSelect.API.Profiles;
using AutoSelect.API.Repositories;
using AutoSelect.API.Repositories.Interfaces;
using AutoSelect.API.Services;
using AutoSelect.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
string connectionString = builder.Configuration.GetConnectionString("Host")!;
builder.Services.AddDbContext<AutoSelectDbContext>(options =>
{
    options.UseLazyLoadingProxies().UseNpgsql(connectionString);
});
builder
    .Services.AddIdentityApiEndpoints<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AutoSelectDbContext>();

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// DI for repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IServiceInfoRepository, ServiceInfoRepository>();

// DI for services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IServiceInfoService, ServiceInfoService>();

// Health Checks
// builder
//     .Services.AddHealthChecks() // appsettings.json
//     .AddNpgSql(
//         connectionString: connectionString,
//         healthQuery: "SELECT 1",
//         name: "NpgSql Check",
//         failureStatus: HealthStatus.Unhealthy,
//         tags: new[] { "sql" }
//     );

builder.Services.AddHealthChecksUI().AddInMemoryStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

// app.MapHealthChecks(
//     "/health",
//     new HealthCheckOptions
//     {
//         Predicate = _ => true,
//         ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
//     }
// );
// app.MapHealthChecksUI();

//   "HealthChecksUI": {
//     "HealthChecks": [
//       {
//         "Name": "AutoSelect",
//         "Uri": "http://localhost:5154/health"
//       }
//     ],
//     "EvaluationTimeinSeconds": 2,
//     "MinimumSecondsBetweenFailureNotifications": 60
//   }

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = Enum.GetNames<Roles>();

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.Run();
