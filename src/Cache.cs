using UnityEngine;

namespace UEIntegration {
    /**
     * <summary>
     * Caches useful objects on scene loads.
     * </summary>
     */
    internal static class Cache {
        internal static Camera playerCamera { get; private set; }

        /**
         * <summary>
         * Finds objects on scene loads.
         * </summary>
         */
        internal static void FindObjects() {
            // Find the main camera through an audio listener
            foreach (AudioListener listener in GameObject.FindObjectsOfType<AudioListener>()) {
                if ("CamY".Equals(listener.name) == false
                    && "MainCamera".Equals(listener.name) == false
                ) {
                    continue;
                }

                playerCamera = listener.GetComponent<Camera>();
                break;
            }
        }

        /**
         * <summary>
         * Clears the cache on scene unloads.
         * </summary>
         */
        internal static void Clear() {
            playerCamera = null;
        }
    }
}
