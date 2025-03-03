using HarmonyLib;

namespace UEIntegration.Patches {
    [HarmonyPatch(typeof(LeavePeakScene), "Update")]
    static class DisableLeavePeakScene {
        static bool Prefix() {
            return ShowUI.allowingMovement;
        }
    }
}
