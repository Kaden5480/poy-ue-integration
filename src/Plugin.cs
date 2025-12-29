using System.Linq;

using BepInEx;
using HarmonyLib;
using ModMenu;
using UILib.Patches;

using UEIntegration.Patches;

namespace UEIntegration {
    [BepInPlugin("com.github.Kaden5480.poy-ue-integration", "UE Integration", PluginInfo.PLUGIN_VERSION)]
    [BepInDependency("com.github.Kaden5480.poy-ui-lib")]
    [BepInDependency(
        "com.github.Kaden5480.poy-mod-menu",
        BepInDependency.DependencyFlags.SoftDependency
    )]
    public class Plugin : BaseUnityPlugin {
        /**
         * <summary>
         * Executes when the plugin is being loaded.
         * </summary>
         */
        public void Awake() {
            // Initialize config
            UEIntegration.Config.Init(this.Config);

            // Add listeners for scene loads/unloads
            SceneLoads.AddLoadListener(delegate {
                Cache.FindObjects();
                Patcher.PatchLate();
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

    }
}
