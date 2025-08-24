using BepInEx;
using HarmonyLib;

namespace Koi.Subnautica.ImprovedScanInfo
{
    /// <summary>
    /// The root mod class.
    /// </summary>
    [BepInPlugin(ModConstants.Meta.Guid, ModConstants.Meta.Name, ModConstants.Meta.Version)]
    public class ModPlugin : BaseUnityPlugin
    {
        /// <summary>
        /// The harmony instance.
        /// </summary>
        private static readonly Harmony Harmony = new(ModConstants.Meta.Guid);

        private void Awake()
        {
            ModLogger.Init(Logger);
            ModConfig.Init(Config);
            ModTranslations.UpdateInGameTranslations();

            Harmony.PatchAll();
        }
    }
}