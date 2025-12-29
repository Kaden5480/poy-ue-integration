using System;
using System.Reflection;

using BepInEx;
using HarmonyLib;
using UnityEngine.SceneManagement;

namespace UEIntegration {
    [BepInPlugin("com.github.Kaden5480.poy-ue-integration", "UE Integration", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        public static Plugin instance;
        public Config.Cfg config;
        public Cache cache { get; } = new Cache();

        private Patches.Patcher patcher = new Patches.Patcher();

        private const float defaultFarClipPlane = 1000f;
        private const float defaultFieldOfView = 60f;


        public void Awake() {
            instance = this;

            config.freecam.farClipPlane = Config.Bind(
                "Freecam", "farClipPlane", defaultFarClipPlane,
                "The default far clip plane for the freecam"
            );
            config.freecam.fieldOfView = Config.Bind(
                "Freecam", "fieldOfView", defaultFieldOfView,
                "The default field of view for the freecam"
            );
            config.freecam.alwaysReapply = Config.Bind(
                "Freecam", "alwaysReapply", false,
                "Whether to always reapply freecam customizations when entering freecam"
            );
            config.freecam.copyPostProcessing = Config.Bind(
                "Freecam", "copyPostProcessing", false,
                "Whether to copy post processing to freecam"
            );

            Harmony.CreateAndPatchAll(typeof(Patches.DisableLeavePeakScene));
            Harmony.CreateAndPatchAll(typeof(Patches.DisableCrampons));
            Harmony.CreateAndPatchAll(typeof(Patches.DisableInventory));
            Harmony.CreateAndPatchAll(typeof(Patches.DisableRope));
            Harmony.CreateAndPatchAll(typeof(Patches.DisableRoutingFlag));

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        public void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            patcher.Patch();
            cache.OnSceneLoaded();
        }

        private void OnSceneUnloaded(Scene scene) {
            cache.OnSceneUnloaded();
        }

        private void Update() {
            Patches.ShowUI.Update();
        }
    }
}
