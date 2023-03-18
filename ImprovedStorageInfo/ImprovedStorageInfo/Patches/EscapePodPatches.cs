using HarmonyLib;

namespace Koi.Subnautica.ImprovedStorageInfo.Patches
{
    /// <summary>
    /// The root harmony patched for Escape Pod game object.
    /// </summary>
    [HarmonyPatch(typeof(EscapePod))]
    public static class EscapePodPatches
    {
        /// <summary>
        /// A post-fix patch on EscapePod.StorageHover.
        /// </summary>
        [HarmonyPatch(nameof(EscapePod.StorageHover))]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        public static void StorageHover(EscapePod __instance)
        {
            if (!ModConfig.ConfigEnabled.Value) return;

            var itemContainer = Utils.ContainerUtils.GetItemContainer(__instance.storageContainer);

            HandReticle.main.SetText(
                HandReticle.TextType.HandSubscript,
                Utils.ContainerUtils.GetCustomInteractText(itemContainer),
                false
            );
        }
    }
}