using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Koi.Subnautica.ModTranslationHelper;

/// <summary>
/// Represents all available translations.
/// </summary>
public class Translations
{
    /// <summary>
    /// The path of folder that contains all translations files.
    /// </summary>
    private readonly string _languagesPath;

    /// <summary>
    /// The translation file of currently selected language.
    /// </summary>
    private TranslationFile _currentTranslationFile;

    /// <summary>
    /// All available translations files data.
    /// </summary>
    private List<TranslationFile> _translationFiles;

    /// <summary>
    /// Create a new instance.
    /// </summary>
    /// <param name="languagesPath">The path of folder that contains all translations files</param>
    public Translations(string languagesPath)
    {
        _languagesPath = languagesPath;

        ReloadAll();
    }

    /// <summary>
    /// Reload all translation data (reload files).
    /// </summary>
    public void ReloadAll()
    {
        var translationFilePaths = Directory.GetFiles(_languagesPath, "*.json", SearchOption.AllDirectories);

        _translationFiles = translationFilePaths
            .Select(filePath => new TranslationFile(filePath))
            .ToList();
    }

    /// <summary>
    /// Reload translation data.
    /// </summary>
    /// <param name="language">The name of language to reload (Reload the current if not specified)</param>
    public void Reload(string language = null)
    {
        if (language == null)
        {
            _currentTranslationFile?.Reload();
        }
        else
        {
            GetTranslationFile(language)?.Reload();
        }
    }

    /// <summary>
    /// Get the filepath of th current active translation file.
    /// </summary>
    /// <returns>The filepath of th current active translation file (NULL if no active translation file)</returns>
    public string GetFilepath()
    {
        return _currentTranslationFile?.Filepath;
    }

    /// <summary>
    /// Get the language of the currently active translation file.
    /// </summary>
    /// <returns>The language of the currently active translation file</returns>
    public string GetLanguage()
    {
        return _currentTranslationFile?.Language;
    }

    /// <summary>
    /// Set the language.
    /// </summary>
    /// <param name="language">The name of language to set</param>
    public bool SetLanguage(string language)
    {
        _currentTranslationFile = GetTranslationFile(language);

        Reload();

        return _currentTranslationFile != null;
    }

    /// <summary>
    /// Get the translation identified by the specified key.
    /// </summary>
    /// <param name="key">The key of translation</param>
    /// <returns>The corresponding translated value (NULL if no translate data or key available)</returns>
    public string GetTranslation(string key)
    {
        return _currentTranslationFile?.Get(key);
    }

    /// <summary>
    /// Get the corresponding translation data of the specified language name.
    /// </summary>
    /// <param name="language">The language name to search the corresponding translation data</param>
    /// <returns>
    /// The corresponding translation data of the specified language name, the default language otherwise
    /// (NULL if no available default language translation file)
    /// </returns>
    private TranslationFile GetTranslationFile(string language)
    {
        var normalizedLanguageName = StringUtils.Normalize(language);

        return _translationFiles.Find(tf => tf.Language.Equals(normalizedLanguageName));
    }
}