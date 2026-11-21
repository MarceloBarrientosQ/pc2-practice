using Microsoft.EntityFrameworkCore;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Infrastructure.Persistence.EFC.Configuration;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>Database context of the BuildingConnected platform.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>Declares how domain date types travel to and from the database.</summary>
    /// <param name="configurationBuilder">Builder that configures the conventions of this context.</param>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>()
            .HaveColumnType("date");
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConstructionTendersConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}