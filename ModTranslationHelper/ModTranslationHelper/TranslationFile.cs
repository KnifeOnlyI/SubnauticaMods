using System;
using System.Collections.Generic;
using System.IO;
using LitJson;

namespace Koi.Subnautica.ModTranslationHelper;

/// <summary>
/// Represent a translation file data.
/// </summary>
internal class TranslationFile
{
    /// <summary>
    /// The translation data.
    /// </summary>
    private readonly Dictionary<string, string> _data = new();

    /// <summary>
    /// The translation file path.
    /// </summary>
    public readonly string Filepath;

    /// <summary>
    /// The language name.
    /// </summary>
    public readonly string Language;

    /// <summary>
    /// Create a new translation file data.
    /// </summary>
    /// <param name="filepath">The filepath of translation file</param>
    public TranslationFile(string filepath)
    {
        Filepath = filepath;

        Language = StringUtils.Normalize(Path.GetFileNameWithoutExtension(Filepath));

        Reload();
    }

    /// <summary>
    /// Reload the translation data.
    /// </summary>
    public void Reload()
    {
        _data.Clear();

        JsonData jsonData;

        using (var streamReader = new StreamReader(Filepath))
        {
            try
            {
                jsonData = JsonMapper.ToObject(streamReader);
            }

            catch (Exception)
            {
                return;
            }
        }

        foreach (var key in jsonData.Keys)
        {
            _data[StringUtils.Normalize(key)] = (string) jsonData[key];
        }
    }

    /// <summary>
    /// Get a translated key identified by the specified key.
    /// </summary>
    /// <param name="key">The key that contains the translation</param>
    /// <returns>The translated string (NULL if no available specified key)</returns>
    public string Get(string key)
    {
        _data.TryGetValue(StringUtils.Normalize(key), out var translation);

        return translation;
    }
}