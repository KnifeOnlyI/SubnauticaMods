using System.IO;
using Koi.Subnautica.ModTranslationHelper;

namespace Koi.Subnautica.ImprovedScanInfo_BZ;

/// <summary>
/// The mod translations.
/// </summary>
public static class ModTranslations
{
    /// <summary>
    /// The translations folder.
    /// </summary>
    private static readonly string TranslationsFolder =
        $"{Path.GetDirectoryName(typeof(ModPlugin).Assembly.Location)}/{ModConstants.Translations.RootFolder}";

    /// <summary>
    /// The translation data.
    /// </summary>
    private static readonly Translations Data = new(TranslationsFolder);

    /// <summary>
    /// The translation for an already synthetized blueprint.
    /// </summary>
    private static string _blueprintAlreadySynthetized;

    /// <summary>
    /// The translation for an already synthetized blueprint.
    /// </summary>
    public static string BlueprintAlreadySynthetized => _blueprintAlreadySynthetized;

    public static void UpdateInGameTranslations()
    {
        if (!Language.isNotQuitting)
        {
            ModLogger.LogWarning("Application is quitting, can't use Language.main");
            return;
        }

        SetLanguage(Language.main.GetCurrentLanguage());

        UpdateBlueprintAlreadySynthetizedTranslation();
    }

    /// <summary>
    /// Set the language.
    /// </summary>
    /// <param name="language">The language</param>
    private static void SetLanguage(string language)
    {
        if (!Data.SetLanguage(language))
        {
            ModLogger.LogError(
                $"Cannot configure mod translations for language `{language}` because " +
                $"translation file `{TranslationsFolder}/{Language.main.GetCurrentLanguage()}.json` is not available."
            );
        }
        else
        {
            Data.ReloadAll();
        }
    }

    /// <summary>
    /// Update the specified translation.
    /// </summary>
    /// <param name="translation">The translation property to update</param>
    /// <param name="translationKey">The translation key</param>
    /// <param name="defaultTranslation">The default translation</param>
    private static void UpdateTranslation(out string translation, string translationKey, string defaultTranslation)
    {
        translation = Data.GetTranslation(translationKey);

        if (translation != null) return;

        if (Data.GetFilepath() != null)
        {
            ModLogger.LogError(
                $"Cannot find a translation key `{translationKey}` in translation file `{Data.GetFilepath()}`."
            );
        }
        else if (Data.GetLanguage() != null)
        {
            ModLogger.LogError(
                $"Cannot find a translated value for key `{translationKey}` because " +
                $"translation file `{TranslationsFolder}/{Language.main.GetCurrentLanguage()}.json` is not available."
            );
        }
        else
        {
            ModLogger.LogError(
                $"Cannot find a translation for key `{translationKey}` because no translation language selected."
            );
        }

        ModLogger.LogWarning($"Set default translated value for key `{translationKey}` to `{defaultTranslation}`");

        translation = defaultTranslation;
    }

    /// <summary>
    /// Update the translation for blueprint already synthetized.
    /// </summary>
    private static void UpdateBlueprintAlreadySynthetizedTranslation()
    {
        UpdateTranslation(
            out _blueprintAlreadySynthetized,
            ModConstants.Translations.Keys.BlueprintAlreadySynthetized.Key,
            ModConstants.Translations.Keys.BlueprintAlreadySynthetized.DefaultValue
        );
    }
}