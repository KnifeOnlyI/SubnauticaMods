using System.Linq;

namespace Koi.Subnautica.ImprovedStorageInfo.core;

/// <summary>
/// Utilitary class for all containers.
/// </summary>
public static class ContainerUtils
{
    /// <summary>
    /// Get the item container if the specified storage container.
    /// </summary>
    /// <param name="storageContainer">The storage container which contains the item container</param>
    /// <returns>The item container (or NULL if no one available)</returns>
    public static ItemsContainer GetItemContainer(StorageContainer storageContainer)
    {
        var item = storageContainer.container;

        if (item == null)
        {
            ModPlugin.Logger.LogWarning("Container don't have an itemsContainer");
        }

        return item;
    }

    /// <summary>
    /// Get the custom interact text corresponding to the specified item container.
    /// </summary>
    /// <param name="itemContainer">The item container to check</param>
    /// <returns>The custom interact text</returns>
    public static string GetCustomInteractText(ItemsContainer itemContainer)
    {
        var customSubscriptText = string.Empty;

        if (itemContainer == null)
        {
            ModPlugin.Logger.LogWarning("Container is null, cannot set a custom interact text");

            return customSubscriptText;
        }

        var containerCapacity = GetContainerCapacity(itemContainer);
        var nbItemsInContainer = GetOccupiedSlotsInContainer(itemContainer);

        var containerIsFull = nbItemsInContainer == containerCapacity;
        var containerIsEmpty = nbItemsInContainer == 0;

        if (containerIsEmpty)
        {
            customSubscriptText = ModPlugin.Translation.Format(
                ModPlugin.Translation.Translate(ModConstants.ContainerEmptyTranslationKey),
                containerCapacity
            );
        }
        else if (containerIsFull)
        {
            customSubscriptText = ModPlugin.Translation.Format(
                ModPlugin.Translation.Translate(ModConstants.ContainerFullTranslationKey),
                containerCapacity,
                containerCapacity
            );
        }
        else
        {
            customSubscriptText = ModPlugin.Translation.Format(
                ModPlugin.Translation.Translate(ModConstants.ContainerNotEmptyTranslationKey),
                nbItemsInContainer,
                containerCapacity
            );
        }

        return customSubscriptText;
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