using BepInEx.Logging;

namespace Koi.Subnautica.ImprovedStorageInfo;

public static class ModLogger
{
    /// <summary>
    /// The mod logger.
    /// </summary>
    private static ManualLogSource _logger;

    public static void Init(ManualLogSource logger)
    {
        _logger = logger;
    }

    public static void LogWarning(string message)
    {
        _logger.LogWarning(message);
    }

    public static void LogError(string message)
    {
        _logger.LogError(message);
    }
}