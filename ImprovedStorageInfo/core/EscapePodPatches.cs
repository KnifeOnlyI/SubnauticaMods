using HarmonyLib;

namespace Koi.Subnautica.ImprovedStorageInfo.core
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
            if (!ModPlugin.ConfigEnabled.Value) return;

            var itemContainer = ContainerUtils.GetItemContainer(__instance.storageContainer);

            HandReticle.main.SetText(
                HandReticle.TextType.HandSubscript,
                ContainerUtils.GetCustomInteractText(itemContainer),
                false
            );
        }
    }
}