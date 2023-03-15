using System;
using System.Collections.Generic;
using System.IO;
using Koi.Subnautica.LoggerUtils;
using LitJson;

namespace Koi.Subnautica.Translation
{
    /// <summary>
    /// An utilitary class for translations.
    /// </summary>
    public class Translation
    {
        private const string DefaultLanguage = "English";

        /// <summary>
        /// The folder that contains all translations files.
        /// </summary>
        private readonly string _languagesFolder;

        /// <summary>
        /// The languages strings.
        /// </summary>
        private readonly Dictionary<string, string> _languageStrings = new Dictionary<string, string>();

        /// <summary>
        /// The logger to use for error logs.
        /// </summary>
        private readonly Logger _logger;

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="languagesFolder">The folder that contains all translations files</param>
        /// <param name="logger">The logger to use for error logs</param>
        public Translation(string languagesFolder, Logger logger)
        {
            _languagesFolder = languagesFolder;
            _logger = logger;
        }

        /// <summary>
        /// Load the language data.
        /// </summary>
        /// <exception cref="Exception">If cannot find any language file</exception>
        private void LoadLanguageData()
        {
            var currentLanguage = Language.main.GetCurrentLanguage();

            if (string.IsNullOrEmpty(currentLanguage))
            {
                currentLanguage = DefaultLanguage;
            }

            var langFile = Path.Combine(_languagesFolder, currentLanguage + ".json");

            if (!File.Exists(langFile))
            {
                langFile = Path.Combine(_languagesFolder, DefaultLanguage + ".json");

                if (!File.Exists(langFile))
                {
                    throw new Exception("Could not find language file");
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
                    _logger.LogError("Failed while loading language json file");

                    return;
                }
            }

            foreach (var key in jsonData.Keys)
            {
                _languageStrings[key] = (string) jsonData[key];
            }
        }

        /// <summary>
        /// Try translate the specified candidate.
        /// </summary>
        /// <param name="candidate">The candidate to translate</param>
        /// <param name="translated">The result</param>
        /// <returns>TRUE if candidate has been translated successfully, FALSE otherwise</returns>
        private bool TryTranslate(string candidate, out string translated)
        {
            if (_languageStrings.TryGetValue(candidate, out translated))
            {
                return true;
            }

            ReloadLanguage();

            return _languageStrings.TryGetValue(candidate, out translated);
        }

        /// <summary>
        /// Translate the specified source.
        /// </summary>
        /// <param name="source">The source to translate</param>
        /// <returns></returns>
        public string Translate(string source)
        {
            if (TryTranslate(source, out var translated))
            {
                return translated;
            }

            _logger.LogError($"Could not find translated string for `{source}`");

            return source;
        }

        /// <summary>
        /// Format the specified translated string.
        /// </summary>
        /// <param name="source">The translated string to format</param>
        /// <param name="args">The args</param>
        /// <returns>The formatted translated string</returns>
        public string Format(string source, params object[] args)
        {
            try
            {
                return string.Format(source, args);
            }
            catch (Exception)
            {
                _logger.LogError($"Failed to format '{source}' with {args}. Return a not formated message");

                return Translate(source);
            }
        }

        /// <summary>
        /// Reload the language data.
        /// </summary>
        private void ReloadLanguage()
        {
            _languageStrings.Clear();

            LoadLanguageData();
        }
    }
}