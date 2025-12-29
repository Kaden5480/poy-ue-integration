using HarmonyLib;

namespace UEIntegration.Patches {
    public class Patcher {
        private bool hasPatched = false;

        public void Patch() {
            if (hasPatched == true) {
                return;
            }

            Harmony.CreateAndPatchAll(typeof(Patches.FreecamDefaults));
        }
    }
}
