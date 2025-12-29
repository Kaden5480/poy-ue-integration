using BepInEx.Configuration;
using ModMenu.Config;

namespace UEIntegration {
    /**
     * <summary>
     * Holds UEIntegration's config.
     * </summary>
     */
    internal static class Config {
        // General
        [Field("Pause Game")]
        internal static ConfigEntry<bool> pauseGame { get; private set; }

        // Freecam
        [Listener(typeof(Patches.FreecamDefaults), nameof(Patches.FreecamDefaults.UpdateCamera))]
        [Field("Field of View")]
        internal static ConfigEntry<float> fov { get; private set; }

        [Listener(typeof(Patches.FreecamDefaults), nameof(Patches.FreecamDefaults.UpdateCamera))]
        [Field("Far Clip Plane")]
        internal static ConfigEntry<float> farClipPlane { get; private set; }

        [Field("Always Reapply")]
        internal static ConfigEntry<bool> alwaysReapply { get; private set; }

        [Listener(typeof(Patches.FreecamDefaults), nameof(Patches.FreecamDefaults.UpdatePostProcess))]
        [Field("Use Post Processing")]
        internal static ConfigEntry<bool> usePostProcess { get; private set; }

        /**
         * <summary>
         * Initializes the config by binding to the
         * provided `ConfigFile`.
         * </summary>
         * <param name="configFile">The config file to bind to</param>
         */
        internal static void Init(ConfigFile configFile) {
            // General
            pauseGame = configFile.Bind(
                "General", "pauseGame", true,
                "Whether the game should pause while Unity Explorer's UI is open."
            );

            // Freecam
            fov = configFile.Bind(
                "Freecam", "fov", 100f,
                "The default field of view for the freecam."
            );

            farClipPlane = configFile.Bind(
                "Freecam", "farClipPlane", 100000f,
                "The default far clip plane for the freecam."
            );

            alwaysReapply = configFile.Bind(
                "Freecam", "alwaysReapply", true,
                "Whether to always reapply freecam customisations when entering freecam."
            );

            usePostProcess = configFile.Bind(
                "Freecam", "usePostProcess", true,
                "Whether to use post processing on the freecam."
            );
        }
    }
}
