using System.Collections.Generic;
using HarmonyLib;

namespace Koi.Subnautica.ImprovedScanInfo.Patches
{
    /// <summary>
    /// Contains all harmony patches for GUI Hand.
    /// </summary>
    [HarmonyPatch(typeof(GUIHand))]
    internal static class GuidHandPatches
    {
        /// <summary>
        /// Harmony patch on : GUIHand.OnUpdate method.
        /// </summary>
        [HarmonyPatch("OnUpdate")]
        [HarmonyPostfix]
        public static void OnUpdate()
        {
            if (!ModConfig.ConfigEnabled.Value) return;

            var scanTarget = PDAScanner.scanTarget;
            var entryData = PDAScanner.GetEntryData(scanTarget.techType);

            if (entryData is not { isFragment: true } || !IsBlueprintAlreadySynthetized(scanTarget.techType)) return;

            HandReticle.main.SetText(
                HandReticle.TextType.HandSubscript,
                ModTranslations.BlueprintAlreadySynthetized,
                false
            );
        }

        /// <summary>
        /// Check if the specified tech type blueprint has been already synthetized.
        /// </summary>
        /// <param name="techType">The tech type of the blueprint to check</param>
        /// <returns>TRUE if the specified tech type blueprint has been already synthetized, FALSE otherwise</returns>
        private static bool IsBlueprintAlreadySynthetized(TechType techType)
        {
            return new Traverse(typeof(PDAScanner))
                .Field<HashSet<TechType>>("complete").Value
                .Contains(techType);
        }
    }
}