using System;
using System.Reflection;

using HarmonyLib;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace UEIntegration.Patches {
    /**
     * <summary>
     * Patches Unity Explorer's freecam to be more sensible.
     * </summary>
     */
    [HarmonyPatch]
    internal static class FreecamDefaults {
        private static Type freeCamPanel;

        private static Camera camera;
        private static PostProcessLayer ourPostProcess;

        /**
         * <summary>
         * Finds the method to patch at runtime.
         * </summary>
         */
        private static MethodBase TargetMethod() {
            freeCamPanel = AccessTools.TypeByName("UnityExplorer.UI.Panels.FreeCamPanel");
            return AccessTools.Method(freeCamPanel, "SetupFreeCamera");
        }

        /**
         * <summary>
         * Updates other camera settings.
         * </summary>
         */
        internal static void UpdateCamera(float _) {
            if (camera == null) {
                return;
            }

            camera.fieldOfView = Config.fov.Value;
            camera.farClipPlane = Config.farClipPlane.Value;
        }

        /**
         * <summary>
         * Updates the enabled state of post processing.
         * </summary>
         * <param name="enabled">Whether post processing should be enabled</param>
         */
        internal static void UpdatePostProcess(bool enabled) {
            if (ourPostProcess == null) {
                return;
            }

            ourPostProcess.enabled = enabled;
        }


        /**
         * <summary>
         * Applies post processing to the freecam.
         * </summary>
         * <param name="camera">The camera to add post processing to</param>
         */
        private static void AddPostProcessing(Camera camera) {
            if (Cache.playerCamera == null) {
                return;
            }

            // Copy camera settings
            camera.renderingPath = Cache.playerCamera.renderingPath;

            // Copy post processing
            PostProcessLayer playerPostProcess = Cache.playerCamera.GetComponent<PostProcessLayer>();
            if (playerPostProcess == null) { return; }

            ourPostProcess = camera.GetComponent<PostProcessLayer>();
            if (ourPostProcess == null) {
                ourPostProcess = camera.gameObject.AddComponent<PostProcessLayer>();
            }

            // Copy specific fields for post processing
            string[] fieldNames = new[] {
                "m_ActiveEffects", "m_Resources", "m_OldResources",
            };

            foreach (string name in fieldNames) {
                FieldInfo info = AccessTools.Field(typeof(PostProcessLayer), name);
                info.SetValue(ourPostProcess, info.GetValue(playerPostProcess));
            }

            ourPostProcess.antialiasingMode = playerPostProcess.antialiasingMode;
            ourPostProcess.volumeLayer = playerPostProcess.volumeLayer;
        }

        /**
         * <summary>
         * Gets the camera before SetupFreeCamera runs and stores it in a state.
         * This provides a way of determining if a new camera was made, or
         * the same one was reused.
         * </summary>
         */
        private static void Prefix(ref Camera __state) {
            __state = (Camera) AccessTools.Field(freeCamPanel, "ourCamera")
                .GetValue(null);
        }

        /**
         * <summary>
         * Applies customisations to Unity Explorer's freecam once it's setup.
         * </summary>
         */
        private static void Postfix(Camera __state) {
            camera = (Camera) AccessTools.Field(freeCamPanel, "ourCamera")
                .GetValue(null);

            // Use post processing
            AddPostProcessing(camera);
            UpdatePostProcess(Config.usePostProcess.Value);

            // The same camera was re-used, but settings shouldn't reapply
            if (__state != null && Config.alwaysReapply.Value == false) {
                return;
            }

            // Otherwise, apply the settings
            camera.fieldOfView = Config.fov.Value;
            camera.farClipPlane = Config.farClipPlane.Value;
        }
    }
}
