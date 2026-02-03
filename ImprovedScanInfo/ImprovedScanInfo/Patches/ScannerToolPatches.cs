using HarmonyLib;
using Koi.Subnautica.ImprovedScanInfo.Utility;

namespace Koi.Subnautica.ImprovedScanInfo.Patches
{
    [HarmonyPatch(typeof(ScannerTool))]
    internal static class ScannerToolPatches
    {
        /// <summary>
        /// Harmony patch applied to: <c>ScannerTool.OnHover()</c>
        /// <para>
        /// Adds hint text to the player's reticle if the player is holding a powered scanner tool and looking at a
        /// fragment of an already-scanned blueprint.
        /// </para>
        /// </summary>
        /// <param name="___energyMixin">Injected: the power status of the scanner tool the player is holding</param>
        /// <param name="___stateCurrent">Injected: the current operation the scanner tool is performing, if any</param>
        [HarmonyPatch("OnHover"), HarmonyPostfix]
        internal static void OnHover(EnergyMixin ___energyMixin, ScannerTool.ScanState ___stateCurrent)
        {
            if (___energyMixin.charge <= 0f || ___stateCurrent == ScannerTool.ScanState.SelfScan) return;

            PDAScanner.ScanTarget target = PDAScanner.scanTarget;

            if (!target.IsValidScanTarget() || !target.IsScanCompleteFragment()) return;

            HandReticle.main.SetText(
                HandReticle.TextType.HandSubscript,
                ModConstants.Translations.Keys.BlueprintAlreadySynthesized,
                translate: true);
        }

        /// <summary>
        /// Determines whether the specified scanner-tool target is valid and scannable.
        /// </summary>
        /// <param name="scanTarget">Any scanner-tool target</param>
        /// <returns><c>true</c> if the target is valid and allows scanning; <c>false</c> otherwise</returns>
        private static bool IsValidScanTarget(this PDAScanner.ScanTarget scanTarget) =>
            scanTarget.isValid && (PDAScanner.CanScan(scanTarget) == PDAScanner.Result.Scan);
    }
}
