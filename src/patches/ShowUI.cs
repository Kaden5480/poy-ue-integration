using UnityEngine;
using UnityExplorer.Config;
using UnityExplorer.UI;

namespace UEIntegration.Patches {
    static class ShowUI {
        private static bool allowingMovement = true;

        private static void AllowMovement(bool allow) {
            allowingMovement = allow;

            Cache cache = Plugin.instance.cache;

            if (cache.playerManager != null) {
                cache.playerManager.AllowPlayerControl(allow);
            }

            if (cache.peakSummited != null) {
                cache.peakSummited.DisableEverythingButClimbing(!allow);
            }
        }

        public static void Update() {
            if (InGameMenu.isLoading == true
                || EnterPeakScene.enteringPeakScene == true
                || EnterPeakScene.enteringAlpScene == true
                || EnterRoomSegmentScene.enteringScene == true
            ) {
                return;
            }

            bool showUI = UIManager.ShowMenu;

            // Toggle movement in very specific cases
            if (showUI == true) {
                AllowMovement(false);
            }
            else if (allowingMovement == false && showUI == false) {
                AllowMovement(true);
            }

            if (showUI == true) {
                InGameMenu.hasBeenInMenu = true;
            }
        }
    }
}
