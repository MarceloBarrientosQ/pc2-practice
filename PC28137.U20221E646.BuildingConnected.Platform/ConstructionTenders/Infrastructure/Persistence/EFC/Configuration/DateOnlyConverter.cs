using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Infrastructure.Persistence.EFC.Configuration;

/// <summary>Stores a calendar date as the type the MySQL provider reads back.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class DateOnlyConverter() : ValueConverter<DateOnly, DateTime>(
    date => date.ToDateTime(TimeOnly.MinValue),
    dateTime => DateOnly.FromDateTime(dateTime));