using System.Linq;
using HarmonyLib;

namespace Koi.Subnautica.ImprovedStorageInfo.core
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
            if (!ModPlugin.ConfigEnabled.Value) return;

            var item = __instance.container;

            if (item == null)
            {
                ModPlugin.LogWarning("Container don't have an itemsContainer");
            }

            SetCustomInteractText(item);
        }

        /// <summary>
        /// Set the sub-text when player hand over on a storage container.
        /// </summary>
        /// <param name="container">The container that handed hover by the player</param>
        private static void SetCustomInteractText(ItemsContainer container)
        {
            var customSubscriptText = string.Empty;

            if (container == null)
            {
                ModPlugin.LogWarning("Container is null, cannot set a custom interact text");

                HandReticle.main.SetText(HandReticle.TextType.HandSubscript, customSubscriptText, false);

                return;
            }

            var containerCapacity = GetContainerCapacity(container);
            var nbItemsInContainer = GetOccupiedSlotsInContainer(container);

            var containerIsFull = nbItemsInContainer == containerCapacity;
            var containerIsEmpty = nbItemsInContainer == 0;

            if (containerIsEmpty)
            {
                customSubscriptText = Translation.Format(
                    Translation.Translate(ModConstants.ContainerEmptyTranslationKey),
                    containerCapacity
                );
            }
            else if (containerIsFull)
            {
                customSubscriptText = Translation.Format(
                    Translation.Translate(ModConstants.ContainerFullTranslationKey),
                    containerCapacity,
                    containerCapacity
                );
            }
            else
            {
                customSubscriptText = Translation.Format(
                    Translation.Translate(ModConstants.ContainerNotEmptyTranslationKey),
                    nbItemsInContainer,
                    containerCapacity
                );
            }

            HandReticle.main.SetText(HandReticle.TextType.HandSubscript, customSubscriptText, false);
        }

        /// <summary>
        /// Get the capacity of the specified container.
        /// </summary>
        /// <param name="container">The container to check</param>
        /// <returns>The capacity of the specified container</returns>
        private static int GetContainerCapacity(ItemsContainer container)
        {
            return container.sizeX * container.sizeY;
        }

        /// <summary>
        /// Get the number of occupied slots in the specified container.
        /// </summary>
        /// <param name="container">The container to check</param>
        /// <returns>the number of occupied slots in the specified container</returns>
        private static int GetOccupiedSlotsInContainer(ItemsContainer container)
        {
            return container.GetItemTypes()
                .Sum(itemType => (from item in container.GetItems(itemType) select item.height * item.width).Sum());
        }
    }
}