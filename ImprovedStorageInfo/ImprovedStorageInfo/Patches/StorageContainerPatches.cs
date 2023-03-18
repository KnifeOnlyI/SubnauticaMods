using HarmonyLib;

namespace Koi.Subnautica.ImprovedStorageInfo.Patches
{
    /// <summary>
    /// The root harmony patched for Storage Container game object.
    /// </summary>
    [HarmonyPatch(typeof(StorageContainer))]
    public static class StorageContainerPatches
    {
        /// <summary>
        /// A post-fix patch on StorageContainer.OnHandHover.
        /// </summary>
        /// <param name="__instance">The storage container</param>
        [HarmonyPatch(nameof(StorageContainer.OnHandHover))]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        public static void OnHandHover(StorageContainer __instance)
        {
            if (!ModConfig.ConfigEnabled.Value) return;

            var itemContainer = Utils.ContainerUtils.GetItemContainer(__instance);

            HandReticle.main.SetText(
                HandReticle.TextType.HandSubscript,
                Utils.ContainerUtils.GetCustomInteractText(itemContainer),
                false
            );
        }
    }
}