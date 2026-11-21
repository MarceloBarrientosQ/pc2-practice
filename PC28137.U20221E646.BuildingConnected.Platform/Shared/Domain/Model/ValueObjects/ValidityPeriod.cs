namespace PC212053.U20221E646.BuildingConnected.Platform.Shared.Domain.Model.ValueObjects;

/// <summary>
///     Closed range of calendar dates during which something remains in force.
/// </summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public class ValidityPeriod
{
    /// <summary>Required by Entity Framework Core.</summary>
    protected ValidityPeriod() { }

    /// <summary>Creates a validity period.</summary>
    /// <param name="startDate">First day of the period.</param>
    /// <param name="endDate">Last day of the period; must be later than the start date.</param>
    /// <exception cref="ArgumentException">When the end date is not later than the start date.</exception>
    public ValidityPeriod(DateOnly startDate, DateOnly endDate)
    {
        if (endDate <= startDate)
            throw new ArgumentException(
                "The end date of a validity period must be later than its start date.", nameof(endDate));

        StartDate = startDate;
        EndDate = endDate;
    }

    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }

    /// <summary>Indicates whether the period is already over on the given date.</summary>
    /// <param name="checkDate">Date to compare against the end of the period.</param>
    /// <returns>True when the date is later than the end date.</returns>
    public bool IsExpired(DateOnly checkDate) => checkDate > EndDate;
}