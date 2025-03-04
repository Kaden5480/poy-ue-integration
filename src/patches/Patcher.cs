using HarmonyLib;

namespace UEIntegration.Patches {
    public class Patcher {
        private bool hasPatched = false;

        public void Patch() {
            if (hasPatched == true) {
                return;
            }

#if BEPINEX
            Harmony.CreateAndPatchAll(typeof(Patches.FreecamDefaults));
#elif MELONLOADER
            Patches.FreecamDefaults.Patch(Plugin.instance.HarmonyInstance);
#endif

            hasPatched = true;
        }
    }
}
