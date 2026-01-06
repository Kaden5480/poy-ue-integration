using System;
using System.Linq;

using BepInEx;
using HarmonyLib;
using ModMenu;
using UILib.Patches;

using UEIntegration.Patches;

namespace UEIntegration {
    [BepInDependency("com.sinai.unityexplorer")]
    [BepInDependency("com.github.Kaden5480.poy-ui-lib")]
    [BepInDependency(
        "com.github.Kaden5480.poy-mod-menu",
        BepInDependency.DependencyFlags.SoftDependency
    )]
    [BepInPlugin("com.github.Kaden5480.poy-ue-integration", "UE Integration", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
        private static Plugin instance;

        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        public void Awake() {
            instance = this;

            // Initialize config
            UEIntegration.Config.Init(this.Config);

            // Add listeners for scene loads/unloads
            SceneLoads.AddLoadListener(delegate {
                Cache.FindObjects();
            });

            SceneLoads.AddUnloadListener(delegate {
                Cache.Clear();
            });

            // Register with Mod Menu as an optional dependency
            if (AccessTools.AllAssemblies().FirstOrDefault(
                    a => a.GetName().Name == "ModMenu"
                ) != null
            ) {
                Register();
            }

            // Apply patches
            Patcher.Patch();
        }

        /**
         * <summary>
         * Registers with Mod Menu.
         * </summary>
         */
        private void Register() {
            ModInfo info = ModManager.Register(this);
            info.Add(typeof(UEIntegration.Config));
        }

        /**
         * <summary>
         * Determines whether to pause the game by
         * checking if Unity Explorer's UI is open.
         * </summary>
         */
        private void Update() {
            Patcher.Update();
        }

        /**
         * <summary>
         * Logs a debug message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        internal static void LogDebug(string message) {
#if DEBUG
            if (instance == null) {
                Console.WriteLine($"[Debug] UEIntegration: {message}");
                return;
            }

            instance.Logger.LogInfo(message);
#else
            if (instance != null) {
                instance.Logger.LogDebug(message);
            }
#endif
        }

        /**
         * <summary>
         * Logs an informational message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        internal static void LogInfo(string message) {
            if (instance == null) {
                Console.WriteLine($"[Info] UEIntegration: {message}");
                return;
            }
            instance.Logger.LogInfo(message);
        }

        /**
         * <summary>
         * Logs an error message.
         * </summary>
         * <param name="message">The message to log</param>
         */
        internal static void LogError(string message) {
            if (instance == null) {
                Console.WriteLine($"[Error] UEIntegration: {message}");
                return;
            }
            instance.Logger.LogError(message);
        }
    }
}
