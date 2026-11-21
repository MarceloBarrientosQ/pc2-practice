namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Model.Aggregates;


/// <summary>Creation and last update timestamps kept for the aggregate root.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public partial class ConstructionTender
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}