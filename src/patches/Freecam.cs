using System;
using System.Reflection;

using HarmonyLib;
using UnityEngine;

#if MELONLOADER
using MelonLoader;
#endif

namespace UEIntegration.Patches {
#if BEPINEX
    [HarmonyPatch]
#endif
    static class FreecamDefaults {
        private static Type freeCamPanel;

#if BEPINEX
        static MethodBase TargetMethod() {
            freeCamPanel = AccessTools.TypeByName("UnityExplorer.UI.Panels.FreeCamPanel");
            return AccessTools.Method(freeCamPanel, "SetupFreeCamera");
        }

#elif MELONLOADER
        public static void Patch(HarmonyLib.Harmony harmony) {
            freeCamPanel = AccessTools.TypeByName("UnityExplorer.UI.Panels.FreeCamPanel");
            MethodInfo setupFreeCamera = AccessTools.Method(freeCamPanel, "SetupFreeCamera");
            MethodInfo postfix = AccessTools.Method(typeof(FreecamDefaults), nameof(FreecamDefaults.Postfix));

            harmony.Patch(setupFreeCamera, null, new HarmonyMethod(postfix), null);
        }

#endif

        static void Prefix(ref Camera __state) {
            __state = (Camera) AccessTools.Field(freeCamPanel, "ourCamera")
                .GetValue(null);
        }

        static void Postfix(Camera __state) {
            Config.Freecam config = Plugin.instance.config.freecam;

            if (__state != null && config.alwaysReapply.Value == false) {
                return;
            }

            Camera camera = (Camera) AccessTools.Field(freeCamPanel, "ourCamera")
                .GetValue(null);

            camera.farClipPlane = config.farClipPlane.Value;
            camera.fieldOfView = config.fieldOfView.Value;
        }
    }
}
