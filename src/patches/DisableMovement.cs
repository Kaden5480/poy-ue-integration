using HarmonyLib;

namespace UEIntegration.Patches {
    [HarmonyPatch(typeof(StemFoot), "Update")]
    static class DisableCrampons {
        static bool Prefix() {
            return ShowUI.allowingMovement;
        }
    }

    [HarmonyPatch(typeof(Inventory), "Update")]
    static class DisableInventory {
        static bool Prefix() {
            return ShowUI.allowingMovement;
        }
    }

    [HarmonyPatch(typeof(RopeAnchor), "RopeHotkey")]
    static class DisableRope {
        static bool Prefix() {
            return ShowUI.allowingMovement;
        }
    }

    [HarmonyPatch(typeof(RoutingFlag), "Update")]
    static class DisableRoutingFlag {
        static bool Prefix() {
            return ShowUI.allowingMovement;
        }
    }
}
