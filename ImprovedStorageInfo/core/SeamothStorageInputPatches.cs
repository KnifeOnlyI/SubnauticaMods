using HarmonyLib;

namespace Koi.Subnautica.ImprovedStorageInfo.core
{
    /// <summary>
    /// The root harmony patched for Seamoth Storage Input game object.
    /// </summary>
    [HarmonyPatch(typeof(SeamothStorageInput))]
    public static class SeamothStorageInputPatches
    {
        /// <summary>
        /// A post-fix patch on SeamothStorageInput.OnHandHover.
        /// </summary>
        /// <param name="__instance">The seamoth storage input</param>
        [HarmonyPatch(nameof(SeamothStorageInput.OnHandHover))]
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        public static void OnHandHover(SeamothStorageInput __instance)
        {
            if (!ModPlugin.ConfigEnabled.Value) return;

            var itemContainer = __instance.seamoth.GetStorageInSlot(__instance.slotID, TechType.VehicleStorageModule);

            HandReticle.main.SetText(
                HandReticle.TextType.HandSubscript,
                ContainerUtils.GetCustomInteractText(itemContainer),
                false
            );
        }
    }
}