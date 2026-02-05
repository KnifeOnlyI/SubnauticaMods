using HarmonyLib;
using Koi.Subnautica.ImprovedScanInfo.Utility;
using UnityEngine;

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
        public static void OnUpdate(GameObject ___activeTarget)
        {
            if (!ModConfig.ConfigEnabled.Value) return;

            // Display hint text only if the player is within "interaction range" of an object.
            // (Scanner tool patch will display hint at a longer range if the player is holding a scanner tool.)
            if (___activeTarget == null) return;

            if (!PDAScanner.scanTarget.IsScanCompleteFragment()) return;

            HandReticle.main.SetText(
                HandReticle.TextType.HandSubscript,
                ModConstants.Translations.Keys.BlueprintAlreadySynthesized,
                translate: true
            );
        }
    }
}