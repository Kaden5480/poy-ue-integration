using System;
using System.Reflection;

using HarmonyLib;
using UnityEngine.SceneManagement;

#if BEPINEX
using BepInEx;

namespace UEIntegration {
    [BepInPlugin("com.github.Kaden5480.poy-ue-integration", "UEIntegration", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        private Harmony HarmonyInstance = new Harmony("UEIntegration.LatePatches");

        public void Awake() {
            config.freecam.farClipPlane = Config.Bind(
                "Freecam", "farClipPlane", defaultFarClipPlane,
                "The default far clip plane for the freecam"
            );
            config.freecam.fieldOfView = Config.Bind(
                "Freecam", "fieldOfView", defaultFieldOfView,
                "The default field of view for the freecam"
            );

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;

            CommonAwake();
        }

        public void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        public void Update() {
            CommonUpdate();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            Harmony.CreateAndPatchAll(typeof(Patches.FreecamDefaults));
            CommonSceneLoad();
        }

        private void OnSceneUnloaded(Scene scene) {
            CommonSceneUnload();
        }

#elif MELONLOADER
using MelonLoader;

[assembly: MelonInfo(typeof(UEIntegration.Plugin), "UEIntegration", PluginInfo.PLUGIN_VERSION, "Kaden5480")]
[assembly: MelonGame("TraipseWare", "Peaks of Yore")]

namespace UEIntegration {
    public class Plugin : MelonMod {
        public override void OnInitializeMelon() {
            string filePath = $"{MelonEnvironment.UserDataDirectory}/com.github.Kaden5480.poy-ue-integration.cfg";
            MelonPreferences_Category freecam = MelonPreferences.CreateCategory("UEIntegration_Freecam");
            freecam.SetFilePath(filePath);

            config.freecam.farClipPlane = freecam.CreateEntry<float>("farClipPlane", defaultFarClipPlane);
            config.freecam.fieldOfView = freecam.CreateEntry<float>("fieldOfView", defaultFieldOfView);

            CommonAwake();
        }

        public override void OnUpdate() {
            CommonUpdate();
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName) {
            Patches.FreecamDefaults.Patch(this.HarmonyInstance);
            CommonSceneLoad();
        }

        public override void OnSceneWasUnloaded(int buildIndex, string sceneName) {
            CommonSceneUnload();
        }
#endif

        public static Plugin instance;
        public Config.Cfg config;
        public Cache cache { get; } = new Cache();

        private const float defaultFarClipPlane = 1000f;
        private const float defaultFieldOfView = 60f;

        private void CommonAwake() {
            instance = this;
        }

        private void CommonUpdate() {
            Patches.ShowUI.Update();
        }

        private void CommonSceneLoad() {
            cache.OnSceneLoaded();
        }

        private void CommonSceneUnload() {
            cache.OnSceneUnloaded();
        }
    }
}
