using BepInEx.Logging;

namespace Koi.Subnautica.LoggerUtils
{
    public class Logger
    {
        /// <summary>
        /// The logger to use for error logs.
        /// </summary>
        private readonly ManualLogSource _handle;

        /// <summary>
        /// The mod name to use in error logs.
        /// </summary>
        private readonly string _modName;

        /// <summary>
        /// The mod version to use in error logs.
        /// </summary>
        private readonly string _modVersion;

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="modName">The folder that contains all translation files</param>
        /// <param name="modVersion">The mod name to use in error logs</param>
        /// <param name="handle">The logger to use</param>
        public Logger(string modName, string modVersion, ManualLogSource handle)
        {
            _modName = modName;
            _modVersion = modVersion;
            _handle = handle;
        }

        /// <summary>
        /// Log a debug message.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogDebug(string message)
        {
            _handle.LogDebug(GetFormattedLogMessage(message));
        }

        /// <summary>
        /// Log an info message.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogInfo(string message)
        {
            _handle.LogInfo(GetFormattedLogMessage(message));
        }

        /// <summary>
        /// Log a message.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogMessage(string message)
        {
            _handle.LogMessage(GetFormattedLogMessage(message));
        }

        /// <summary>
        /// Log a warning message.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogWarning(string message)
        {
            _handle.LogWarning(GetFormattedLogMessage(message));
        }

        /// <summary>
        /// Log an error message.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogError(string message)
        {
            _handle.LogError(GetFormattedLogMessage(message));
        }

        /// <summary>
        /// Log a fatal message.
        /// </summary>
        /// <param name="message">The message to log</param>
        public void LogFatal(string message)
        {
            _handle.LogFatal(GetFormattedLogMessage(message));
        }

        /// <summary>
        /// Get a formatted log message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The corresponding formatted log message</returns>
        private string GetFormattedLogMessage(string message)
        {
            return $"{_modName} (v{_modVersion}) : {message}";
        }
    }
}