using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Application.Internal.CommandServices;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Repositories;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Services;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Domain.Repositories;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Repositories;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Interfaces.ASP.Configuration;
using Scalar.AspNetCore;
using UnitOfWork = PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Infrastructure.Persistence.EFC.Repositories.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddOpenApi();

// Error and validation messages follow the Accept-Language header of the request.
var supportedCultures = new[]
{
    new CultureInfo("en"), new CultureInfo("en-US"),
    new CultureInfo("es"), new CultureInfo("es-PE")
};
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(connectionString!).EnableDetailedErrors());

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IConstructionTenderRepository, ConstructionTenderRepository>();
builder.Services.AddScoped<IConstructionTenderCommandService, ConstructionTenderCommandService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.EnsureCreated();

app.UseRequestLocalization();
app.UseExceptionHandler();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();
app.MapControllers();
app.Run();