using UnityEngine;

namespace UEIntegration {
    public class Cache {
        public InGameMenu inGameMenu;
        public PlayerManager playerManager;
        public PeakSummited peakSummited;

        public void OnSceneLoaded() {
            inGameMenu = GameObject.FindObjectOfType<InGameMenu>();
            playerManager = GameObject.FindObjectOfType<PlayerManager>();
            peakSummited = GameObject.FindObjectOfType<PeakSummited>();
        }

        public void OnSceneUnloaded() {
            inGameMenu = null;
            playerManager = null;
            peakSummited = null;
        }
    }
}
