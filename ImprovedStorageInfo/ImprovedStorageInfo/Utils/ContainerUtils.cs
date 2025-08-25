﻿using System.Linq;
using Koi.Subnautica.ImprovedStorageInfo.Config;

namespace Koi.Subnautica.ImprovedStorageInfo.Utils;

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
        return storageContainer.container;
    }

    /// <summary>
    /// Get the custom interacting text corresponding to the specified item container.
    /// </summary>
    /// <param name="itemContainer">The item container to check</param>
    /// <returns>The custom interact text</returns>
    public static string GetCustomInteractText(ItemsContainer itemContainer)
    {
        if (itemContainer == null)
        {
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
    /// <param name="containerIsEmpty">TRUE if the container is empty, FALSE otherwise</param>
    /// <param name="containerIsFull">TRUE if the container is full, FALSE otherwise</param>
    /// <returns>The corresponding translation</returns>
    private static string GetTranslation(bool containerIsEmpty, bool containerIsFull)
    {
        if (containerIsEmpty)
        {
            return ModTranslations.TranslationsHandler.GetTranslation(
                ModConstants.Translations.Keys.ContainerEmpty.Key,
                ModConstants.Translations.Keys.ContainerEmpty.DefaultValue
            );
        }

        return containerIsFull
            ? ModTranslations.TranslationsHandler.GetTranslation(
                ModConstants.Translations.Keys.ContainerFull.Key,
                ModConstants.Translations.Keys.ContainerFull.DefaultValue
            )
            : ModTranslations.TranslationsHandler.GetTranslation(
                ModConstants.Translations.Keys.ContainerNotEmpty.Key,
                ModConstants.Translations.Keys.ContainerNotEmpty.DefaultValue
            );
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