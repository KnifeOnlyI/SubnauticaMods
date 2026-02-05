using System.Collections.Generic;
using HarmonyLib;

namespace Koi.Subnautica.ImprovedScanInfo.Extensions
{
    /// <summary>
    /// Utility extension methods used in multiple patch files.
    /// </summary>
    internal static class UtilityExtensions
    {
        /// <summary>
        /// Determines if a given scanner-tool target is a fragment for a blueprint already in the player's PDA.
        /// </summary>
        /// <param name="scanTarget">Any <em>valid</em> scanner-tool target</param>
        /// <returns><c>true</c> if the conditions stated in the summary are met; <c>false</c> otherwise</returns>
        internal static bool IsScanCompleteFragment(this PDAScanner.ScanTarget scanTarget)
        {
            var entryData = PDAScanner.GetEntryData(scanTarget.techType);
            return entryData.isFragment && entryData.key.IsBlueprintAlreadySynthesized();
        }

        /// <summary>
        /// Check if the specified tech type blueprint has been already synthesized.
        /// </summary>
        /// <param name="techType">The tech type of the blueprint to check</param>
        /// <returns>TRUE if the specified tech type blueprint has been already synthesized, FALSE otherwise</returns>
        private static bool IsBlueprintAlreadySynthesized(this TechType techType)
        {
            return new Traverse(typeof(PDAScanner))
                .Field<HashSet<TechType>>("complete").Value
                .Contains(techType);
        }
    }
}
