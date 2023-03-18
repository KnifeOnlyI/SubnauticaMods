namespace Koi.Subnautica.ImprovedStorageInfo;

/// <summary>
/// To manage mod events.
/// </summary>
public static class ModEvents
{
    /// <summary>
    /// Initialize.
    /// </summary>
    public static void Init()
    {
        Language.OnLanguageChanged += OnLanguageChanges;
    }

    /// <summary>
    /// Executed when language changed.
    /// </summary>
    private static void OnLanguageChanges()
    {
        ModTranslations.SetLanguage(Language.main.GetCurrentLanguage());

        ModTranslations.UpdateContainerEmptyTranslation();
        ModTranslations.UpdateContainerFullTranslation();
        ModTranslations.UpdateContainerNotEmptyTranslation();
    }
}