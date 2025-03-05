using System;
using System.Reflection;

using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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

        private static void ApplyPostProcessing(Camera camera) {
            // Get the normal camera
            GameObject mainCamera = GameObject.Find("MainCamera");
            if (mainCamera == null) {
                mainCamera = GameObject.Find("CamY");
            }

            if (mainCamera == null) { return; }

            // Copy camera settings
            Camera originalCamera = mainCamera.GetComponent<Camera>();
            if (originalCamera == null) { return; }

            camera.renderingPath = originalCamera.renderingPath;

            // Copy post processing
            PostProcessLayer originalLayer = mainCamera.GetComponent<PostProcessLayer>();
            if (originalLayer == null) { return; }

            PostProcessLayer newLayer = camera.gameObject.GetComponent<PostProcessLayer>();
            if (newLayer == null) {
                newLayer = camera.gameObject.AddComponent<PostProcessLayer>();
            }

            string[] fieldNames = new[] {
                "m_ActiveEffects", "m_Resources", "m_OldResources",
            };

            foreach (string name in fieldNames) {
                FieldInfo info = AccessTools.Field(typeof(PostProcessLayer), name);
                info.SetValue(newLayer, info.GetValue(originalLayer));
            }

            newLayer.antialiasingMode = originalLayer.antialiasingMode;
            newLayer.volumeLayer = originalLayer.volumeLayer;
        }

        static void Prefix(ref Camera __state) {
            __state = (Camera) AccessTools.Field(freeCamPanel, "ourCamera")
                .GetValue(null);
        }

        static void Postfix(Camera __state) {
            Config.Freecam config = Plugin.instance.config.freecam;

            Camera camera = (Camera) AccessTools.Field(freeCamPanel, "ourCamera")
                .GetValue(null);

            if (config.copyPostProcessing.Value == true) {
                ApplyPostProcessing(camera);
            }

            if (__state != null && config.alwaysReapply.Value == false) {
                return;
            }

            camera.farClipPlane = config.farClipPlane.Value;
            camera.fieldOfView = config.fieldOfView.Value;
        }
    }
}
