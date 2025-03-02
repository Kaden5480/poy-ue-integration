using UnityEngine;
using UnityExplorer.UI;

namespace UEIntegration.Patches {
    static class ShowUI {
        public static void Update() {
            Cache cache = Plugin.instance.cache;

            bool showUI = UIManager.ShowMenu;

            if (cache.playerManager != null) {
                cache.playerManager.AllowPlayerControl(!showUI);
            }

            if (cache.peakSummited != null) {
                cache.peakSummited.DisableEverythingButClimbing(showUI);
            }

            if (showUI == true) {
                InGameMenu.hasBeenInMenu = true;
            }
        }
    }
}
