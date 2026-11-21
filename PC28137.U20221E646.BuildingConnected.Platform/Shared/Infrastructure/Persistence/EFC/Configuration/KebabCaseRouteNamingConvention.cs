using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>Writes every controller route in kebab case.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public partial class KebabCaseRouteNamingConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        foreach (var selector in controller.Selectors.Where(s => s.AttributeRouteModel is not null))
            selector.AttributeRouteModel!.Template =
                ToKebabCase(selector.AttributeRouteModel.Template ?? string.Empty);
    }

    private static string ToKebabCase(string value) =>
        CamelBoundary().Replace(value, "$1-$2").ToLowerInvariant();

    [GeneratedRegex("([a-z0-9])([A-Z])")]
    private static partial Regex CamelBoundary();
}