using HarmonyLib;

namespace UEIntegration.Patches {
    /**
     * <summary>
     * Handles applying patches.
     * </summary>
     */
    internal static class Patcher {
        /**
         * <summary>
         * Applies patches.
         * </summary>
         */
        internal static void Patch() {
            Harmony.CreateAndPatchAll(typeof(FreecamDefaults));
            Harmony.CreateAndPatchAll(typeof(HidePanels));
        }

        /**
         * <summary>
         * Runs patches that need to run each frame.
         * </summary>
         */
        internal static void Update() {
            UICheck.Update();
        }
    }
}
