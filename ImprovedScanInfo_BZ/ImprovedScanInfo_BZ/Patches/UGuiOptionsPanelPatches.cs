using HarmonyLib;

namespace Koi.Subnautica.ImprovedScanInfo_BZ.Patches
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel))]
    public static class UGuiOptionsPanelPatches
    {
        /// <summary>
        /// This method is called after the game settings are saved to update mod's translations.
        /// </summary>
        [HarmonyPatch(nameof(uGUI_OptionsPanel.OnSave))]
        [HarmonyPostfix]
        public static void OnSave()
        {
            ModTranslations.UpdateInGameTranslations();
        }
    }
}