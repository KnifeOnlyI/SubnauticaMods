using System.IO;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Koi.Subnautica.LoggerUtils;

namespace Koi.Subnautica.ImprovedScanInfo.core
{
    /// <summary>
    /// The root mod class.
    /// </summary>
    [BepInPlugin(ModConstants.Guid, ModConstants.Name, ModConstants.Version)]
    public class ModPlugin : BaseUnityPlugin
    {
        /// <summary>
        /// The harmony instance.
        /// </summary>
        private static readonly Harmony Harmony = new(ModConstants.Guid);

        /// <summary>
        /// The logger.
        /// </summary>
        public new static Logger Logger { get; private set; }

        /// <summary>
        /// To manage translations.
        /// </summary>
        public static Translation.Translation Translation { get; private set; }

        /// <summary>
        /// The configuration entry to indicates if the mod must be enabled or not.
        /// </summary>
        public static ConfigEntry<bool> ConfigEnabled { get; private set; }

        private void Awake()
        {
            ConfigEnabled = Config.Bind("General", "Enabled", true, "TRUE to enable the mod, FALSE otherwise.");

            Harmony.PatchAll();

            Logger = new Logger(ModConstants.Version, base.Logger);

            Translation = new(
                $"{Path.GetDirectoryName(typeof(ModPlugin).Assembly.Location)}/{ModConstants.LanguagesFolder}",
                Logger
            );
        }
    }
}