using System.Linq;

namespace Koi.Subnautica.ImprovedStorageInfo_BZ.Utils;

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
            ModLogger.LogWarning("Container don't have an itemsContainer");
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
        if (itemContainer == null)
        {
            ModLogger.LogWarning("Container is null, cannot set a custom interact text");

            return string.Empty;
        }

        var containerCapacity = GetContainerCapacity(itemContainer);
        var nbItemsInContainer = GetOccupiedSlotsInContainer(itemContainer);

        var containerIsEmpty = nbItemsInContainer == 0;
        var containerIsFull = nbItemsInContainer == containerCapacity;

        var translation = GetTranslation(containerIsEmpty, containerIsFull);

        if (translation == null) return string.Empty;

        return containerIsEmpty
            ? string.Format(translation, containerCapacity)
            : string.Format(translation, nbItemsInContainer, containerCapacity);
    }

    /// <summary>
    /// Get the corresponding translation key based on the specified container data.
    /// </summary>
    /// <param name="containerIsEmpty">TRUE if the container is empty, FALSE othewise</param>
    /// <param name="containerIsFull">TRUE if container is full, FALSE otherwise</param>
    /// <returns>The corresponding translation</returns>
    private static string GetTranslation(bool containerIsEmpty, bool containerIsFull)
    {
        if (containerIsEmpty)
        {
            return ModTranslations.ContainerEmptyTranslation;
        }

        return containerIsFull
            ? ModTranslations.ContainerFullTranslation
            : ModTranslations.ContainerNotEmptyTranslation;
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