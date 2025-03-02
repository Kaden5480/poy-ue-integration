using UnityEngine;

namespace UEIntegration {
    public class Cache {
        public PlayerManager playerManager;
        public PeakSummited peakSummited;

        public void OnSceneLoaded() {
            playerManager = GameObject.FindObjectOfType<PlayerManager>();
            peakSummited = GameObject.FindObjectOfType<PeakSummited>();
        }

        public void OnSceneUnloaded() {
            playerManager = null;
            peakSummited = null;
        }
    }
}
