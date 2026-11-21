using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>Applies database naming conventions to the whole model.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public static class ModelBuilderExtensions
{
    /// <summary>Renames tables and columns to plural snake case.</summary>
    /// <param name="builder">Model builder of the database context.</param>
    public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
    {
        foreach (var entity in builder.Model.GetEntityTypes())
        {
            var tableName = entity.GetTableName();
            if (!string.IsNullOrEmpty(tableName))
                entity.SetTableName(ToPlural(ToSnakeCase(tableName)));

            foreach (var property in entity.GetProperties())
            {
                var columnName = property.GetColumnName();
                if (!string.IsNullOrEmpty(columnName))
                    property.SetColumnName(ToSnakeCase(columnName));
            }

            foreach (var key in entity.GetKeys())
            {
                var keyName = key.GetName();
                if (!string.IsNullOrEmpty(keyName)) key.SetName(ToSnakeCase(keyName));
            }

            foreach (var foreignKey in entity.GetForeignKeys())
            {
                var constraintName = foreignKey.GetConstraintName();
                if (!string.IsNullOrEmpty(constraintName))
                    foreignKey.SetConstraintName(ToSnakeCase(constraintName));
            }

            foreach (var index in entity.GetIndexes())
            {
                var indexName = index.GetDatabaseName();
                if (!string.IsNullOrEmpty(indexName)) index.SetDatabaseName(ToSnakeCase(indexName));
            }
        }
    }

    private static string ToSnakeCase(string text)
    {
        var builder = new StringBuilder(text.Length + 8);
        for (var i = 0; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && i > 0 && text[i - 1] != '_') builder.Append('_');
            builder.Append(char.ToLowerInvariant(text[i]));
        }
        return builder.ToString();
    }

    private static string ToPlural(string text) =>
        text.EndsWith('y') ? string.Concat(text.AsSpan(0, text.Length - 1), "ies")
        : text.EndsWith('s') ? text
        : text + "s";
}