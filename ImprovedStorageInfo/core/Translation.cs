using System;
using System.Collections.Generic;
using System.IO;
using LitJson;

namespace Koi.Subnautica.ImprovedStorageInfo.core
{
    /// <summary>
    /// An utilitary class for translations.
    /// </summary>
    public static class Translation
    {
        /// <summary>
        /// The languages strings.
        /// </summary>
        private static readonly Dictionary<string, string> LanguageStrings = new Dictionary<string, string>();

        /// <summary>
        /// The assembly directory.
        /// </summary>
        private static string GetAssemblyDirectory => Path.GetDirectoryName(typeof(Translation).Assembly.Location);

        /// <summary>
        /// Load the language data.
        /// </summary>
        /// <exception cref="Exception">If cannot find any language file</exception>
        private static void LoadLanguageData()
        {
            var currentLanguage = Language.main.GetCurrentLanguage();

            if (string.IsNullOrEmpty(currentLanguage))
            {
                currentLanguage = ModConstants.DefaultLanguage;
            }

            var langFolder = Path.Combine(GetAssemblyDirectory, ModConstants.LanguagesFolder);
            var langFile = Path.Combine(langFolder, currentLanguage + ".json");

            if (!File.Exists(langFile))
            {
                langFile = Path.Combine(langFolder, ModConstants.DefaultLanguage + ".json");

                if (!File.Exists(langFile))
                {
                    throw new Exception(ModPlugin.GetFormattedLogMessage("Could not find language file"));
                }
            }

            JsonData jsonData;

            using (var streamReader = new StreamReader(langFile))
            {
                try
                {
                    jsonData = JsonMapper.ToObject(streamReader);
                }

                catch (Exception)
                {
                    ModPlugin.LogError("Failed while loading language json file");

                    return;
                }
            }

            foreach (var key in jsonData.Keys)
            {
                LanguageStrings[key] = (string) jsonData[key];
            }
        }

        /// <summary>
        /// Try translate the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate to translate</param>
        /// <param name="translated">The result</param>
        /// <returns>TRUE if candidate has been translated successfully, FALSE otherwise</returns>
        private static bool TryTranslate(string candidate, out string translated)
        {
            if (LanguageStrings.TryGetValue(candidate, out translated))
            {
                return true;
            }

            ReloadLanguage();

            return LanguageStrings.TryGetValue(candidate, out translated);
        }

        /// <summary>
        /// Translate the specified source.
        /// </summary>
        /// <param name="source">The source to translate</param>
        /// <returns></returns>
        public static string Translate(string source)
        {
            if (TryTranslate(source, out var translated))
            {
                return translated;
            }

            ModPlugin.LogError($"Could not find translated string for `{source}`");

            return source;
        }

        /// <summary>
        /// Format the specified translated string.
        /// </summary>
        /// <param name="source">The translated string to format</param>
        /// <param name="args">The args</param>
        /// <returns>The formatted translated string</returns>
        internal static string Format(string source, params object[] args)
        {
            try
            {
                return string.Format(source, args);
            }
            catch (Exception)
            {
                ModPlugin.LogError($"Failed to format '{source}' with {args}. Return a not formated message");

                return Translate(source);
            }
        }

        /// <summary>
        /// Reload the language data.
        /// </summary>
        private static void ReloadLanguage()
        {
            LanguageStrings.Clear();

            LoadLanguageData();
        }
    }
}