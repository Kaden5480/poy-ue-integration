using UnityEngine;
using UnityExplorer.Config;
using UnityExplorer.UI;

namespace UEIntegration.Patches {
    static class ShowUI {
        public static bool allowingMovement = true;

        private static bool IsInMenu() {
            Cache cache = Plugin.instance.cache;

            if (cache.inGameMenu == null) {
                return false;
            }

            return cache.inGameMenu.isMainMenu == true
                || cache.inGameMenu.inMenu == true
                || InGameMenu.isCurrentlyNavigationMenu == true;
        }

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
                InGameMenu.hasBeenInMenu = true;
            }
            else if (allowingMovement == false
                && showUI == false
                && IsInMenu() == false
            ) {
                AllowMovement(true);
            }
        }
    }
}
