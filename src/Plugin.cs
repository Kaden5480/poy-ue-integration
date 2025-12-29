using BepInEx;
using HarmonyLib;
using ModMenu;

namespace UEIntegration {
    [BepInPlugin("com.github.Kaden5480.poy-ue-integration", "UE Integration", PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin {
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
                Patches.Patcher.PatchLate();
            });

            SceneLoads.AddUnloadListener(delegate {
                Cache.Clear();
            });

            // Register with mod menu
            ModInfo info = ModManager.Register(this);
            info.Add(typeof(UEIntegration.Config));
        }
    }
}
