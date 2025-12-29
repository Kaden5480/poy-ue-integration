using BepInEx.Configuration;

namespace UEIntegration.Config {
    public struct Freecam {
        public ConfigEntry<float> farClipPlane;
        public ConfigEntry<float> fieldOfView;
        public ConfigEntry<bool> alwaysReapply;
        public ConfigEntry<bool> copyPostProcessing;
    }
}
