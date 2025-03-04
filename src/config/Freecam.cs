#if BEPINEX
using BepInEx.Configuration;

#elif MELONLOADER
using MelonLoader;

#endif

namespace UEIntegration.Config {
    public struct Freecam {
#if BEPINEX
        public ConfigEntry<float> farClipPlane;
        public ConfigEntry<float> fieldOfView;
        public ConfigEntry<bool> alwaysReapply;

#elif MELONLOADER
        public MelonPreferences_Entry<float> farClipPlane;
        public MelonPreferences_Entry<float> fieldOfView;
        public MelonPreferences_Entry<bool> alwaysReapply;

#endif
    }
}
