using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace fr.koi.tests.core
{
    [BepInPlugin(MyGuid, PluginName, VersionString)]
    public class TestsPlugin : BaseUnityPlugin
    {
        private const string MyGuid = "KOI_Tests";
        private const string PluginName = "KOI Tests";
        private const string VersionString = "1.0.0";

        private static readonly Harmony Harmony = new Harmony(MyGuid);

        public new static ManualLogSource Logger { get; private set; }

        private void Awake()
        {
            Harmony.PatchAll();

            Logger = base.Logger;

            Logger.LogInfo(PluginName + " " + VersionString + " " + "loaded.");
        }
    }
}