using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace Koi.Subnautica.ImprovedStorageInfo.core
{
    [BepInPlugin(ModConstants.Guid, ModConstants.Name, ModConstants.Version)]
    public class ModPlugin : BaseUnityPlugin
    {
        private static readonly Harmony Harmony = new(ModConstants.Guid);

        private new static ManualLogSource Logger { get; set; }

        /// <summary>
        /// The configuration entry to indicates if the mod must be enabled or not.
        /// </summary>
        public static ConfigEntry<bool> ConfigEnabled { get; private set; }

        private void Awake()
        {
            ConfigEnabled = Config.Bind("General", "Enabled", true, "TRUE to enable the mod, FALSE otherwise.");

            Harmony.PatchAll();

            Logger = base.Logger;

            LogInfo("Mod loaded successfully");
        }

        /// <summary>
        /// Log an info.
        /// </summary>
        /// <param name="message">The message to log</param>
        private static void LogInfo(string message)
        {
            Logger.LogInfo(GetFormattedLogMessage(message));
        }

        /// <summary>
        /// Log a warning.
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void LogWarning(string message)
        {
            Logger.LogWarning(GetFormattedLogMessage(message));
        }

        /// <summary>
        /// Log an error.
        /// </summary>
        /// <param name="message">The message to log</param>
        public static void LogError(string message)
        {
            Logger.LogError(GetFormattedLogMessage(message));
        }

        /// <summary>
        /// Get a formatted log message.
        /// </summary>
        /// <param name="message">The message</param>
        /// <returns>The corresponding formatted log message</returns>
        public static string GetFormattedLogMessage(string message)
        {
            return $"{ModConstants.Name} (v{ModConstants.Version}) : {message}";
        }
    }
}