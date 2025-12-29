using HarmonyLib;

namespace UEIntegration.Patches {
    /**
     * <summary>
     * Handles applying patches.
     * </summary>
     */
    internal static class Patcher {
        // Tracks whether late patches have applied yet
        private static bool hasPatched = false;

        /**
         * <summary>
         * Patches to apply later in the game to
         * make sure Unity Explorer has loaded first.
         * </summary>
         */
        internal static void PatchLate() {
            if (hasPatched == true) {
                return;
            }

            Harmony.CreateAndPatchAll(typeof(FreecamDefaults));
            hasPatched = true;
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
