using System.Text;
using Microsoft.EntityFrameworkCore;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Aggregates;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Entity configuration of the Construction Tenders bounded context.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public static class ModelBuilderExtensions
{
    /// <summary>Registers the entity configuration of this context with the model builder.</summary>
    /// <param name="builder">Model builder of the database context.</param>
    public static void ApplyConstructionTendersConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ConstructionTender>().HasKey(t => t.Id);
        builder.Entity<ConstructionTender>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ConstructionTender>().Property(t => t.Title).IsRequired().HasMaxLength(150);
        builder.Entity<ConstructionTender>().Property(t => t.Status).IsRequired().HasConversion<string>();
        builder.Entity<ConstructionTender>().Property(t => t.CreatedAt);
        builder.Entity<ConstructionTender>().Property(t => t.UpdatedAt);

        builder.Entity<ConstructionTender>().OwnsOne(t => t.ConstructionTenderIdentifier, identifier =>
        {
            identifier.WithOwner().HasForeignKey("Id");
            identifier.Property(i => i.Value).HasColumnName("ConstructionTenderIdentifier").IsRequired();
            identifier.HasIndex(i => i.Value).IsUnique();
        });

        builder.Entity<ConstructionTender>().OwnsOne(t => t.ContractingEntityId, entity =>
        {
            entity.WithOwner().HasForeignKey("Id");
            entity.Property(e => e.Value).HasColumnName("ContractingEntityId").IsRequired();
        });

        builder.Entity<ConstructionTender>().OwnsOne(t => t.Period, period =>
        {
            period.WithOwner().HasForeignKey("Id");
            period.Property(p => p.StartDate).HasColumnName("PeriodStartDate").IsRequired();
            period.Property(p => p.EndDate).HasColumnName("PeriodEndDate").IsRequired();
        });

        builder.Entity<ConstructionTender>().OwnsOne(t => t.EstimatedValue, amount =>
        {
            amount.WithOwner().HasForeignKey("Id");
            amount.Property(a => a.Value)
                .HasColumnName("EstimatedValue").HasColumnType("decimal(18,2)").IsRequired();
            amount.Property(a => a.Currency)
                .HasColumnName("EstimatedCurrency").HasMaxLength(3).IsRequired();
        });
    }
}