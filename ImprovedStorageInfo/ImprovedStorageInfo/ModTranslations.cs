using System.IO;
using Koi.Subnautica.ModTranslationHelper;

namespace Koi.Subnautica.ImprovedStorageInfo;

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
    /// The translation for an empty container.
    /// </summary>
    private static string _containerEmptyTranslation;

    /// <summary>
    /// The translation for a full container.
    /// </summary>
    private static string _containerFullTranslation;

    /// <summary>
    /// The translation for a not empty container.
    /// </summary>
    private static string _containerNotEmptyTranslation;

    /// <summary>
    /// The translation for an empty container.
    /// </summary>
    public static string ContainerEmptyTranslation => _containerEmptyTranslation;

    /// <summary>
    /// The translation for a full container.
    /// </summary>
    public static string ContainerFullTranslation => _containerFullTranslation;

    /// <summary>
    /// The translation for a not empty container.
    /// </summary>
    public static string ContainerNotEmptyTranslation => _containerNotEmptyTranslation;

    /// <summary>
    /// Set the language.
    /// </summary>
    /// <param name="language">The language</param>
    public static void SetLanguage(string language)
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
    /// Update the translation for empty containers.
    /// </summary>
    public static void UpdateContainerEmptyTranslation()
    {
        UpdateTranslation(
            out _containerEmptyTranslation,
            ModConstants.Translations.Keys.ContainerEmpty.Key,
            ModConstants.Translations.Keys.ContainerEmpty.DefaultValue
        );
    }

    /// <summary>
    /// Update the translation for full containers.
    /// </summary>
    public static void UpdateContainerFullTranslation()
    {
        UpdateTranslation(
            out _containerFullTranslation,
            ModConstants.Translations.Keys.ContainerFull.Key,
            ModConstants.Translations.Keys.ContainerFull.DefaultValue
        );
    }

    /// <summary>
    /// Update the translation for not empty containers.
    /// </summary>
    public static void UpdateContainerNotEmptyTranslation()
    {
        UpdateTranslation(
            out _containerNotEmptyTranslation,
            ModConstants.Translations.Keys.ContainerNotEmpty.Key,
            ModConstants.Translations.Keys.ContainerNotEmpty.DefaultValue
        );
    }
}