using System.Globalization;

namespace PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Localization;

/// <summary>Keys of the messages the API returns.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public enum MessageKey
{
    TenderAlreadyExists,
    InvalidRequest,
    UnexpectedError
}

/// <summary>Returns API messages in the culture negotiated for the request.</summary>
/// <remarks>Marcelo Barrientos Quispe</remarks>
public static class MessageCatalog
{
    private static readonly Dictionary<MessageKey, string> English = new()
    {
        [MessageKey.TenderAlreadyExists] = "A construction tender with that identifier already exists.",
        [MessageKey.InvalidRequest] = "The submitted data is not valid.",
        [MessageKey.UnexpectedError] = "The request could not be processed."
    };

    private static readonly Dictionary<MessageKey, string> Spanish = new()
    {
        [MessageKey.TenderAlreadyExists] = "Ya existe una licitación de construcción con ese identificador.",
        [MessageKey.InvalidRequest] = "Los datos enviados no son válidos.",
        [MessageKey.UnexpectedError] = "No se pudo procesar la solicitud."
    };

    /// <summary>Reads a message in the culture of the current request.</summary>
    /// <param name="key">Message to read.</param>
    /// <returns>The message in Spanish when Spanish was requested, otherwise in English.</returns>
    public static string Get(MessageKey key) =>
        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "es" ? Spanish[key] : English[key];
}