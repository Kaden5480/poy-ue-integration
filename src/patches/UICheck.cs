using UILib.Patches;
using UnityExplorer.UI;

namespace UEIntegration {
    internal static class UICheck {
        // The pause lock
        private static Lock @lock;

        /**
         * <summary>
         * Runs each frame, checking the status of Unity Explorer's UI.
         * </summary>
         */
        internal static void Update() {
            // Create a lock
            if (Config.pauseGame.Value == true
                && UIManager.ShowMenu == true
                && @lock == null
            ) {
                @lock = new Lock();
            }

            // Close the lock
            if ((Config.pauseGame.Value == false || UIManager.ShowMenu == false)
                && @lock != null
            ) {
                @lock.Close();
                @lock = null;
            }
        }
    }
}
