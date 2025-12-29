using BepInEx.Configuration;
using ModMenu.Config;

namespace UEIntegration {
    /**
     * <summary>
     * Holds UEIntegration's config.
     * </summary>
     */
    internal static class Config {
        [Field("Field of View")]
        internal static ConfigEntry<float> fov { get; private set; }

        [Field("Far Clip Plane")]
        internal static ConfigEntry<float> farClipPlane { get; private set; }

        [Field("Always Reapply")]
        internal static ConfigEntry<bool> alwaysReapply { get; private set; }

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
            fov = Config.Bind(
                "Freecam", "fov", 100f,
                "The default field of view for the freecam."
            );

            farClipPlane = Config.Bind(
                "Freecam", "farClipPlane", 100000f,
                "The default far clip plane for the freecam."
            );

            alwaysReapply = Config.Bind(
                "Freecam", "alwaysReapply", true,
                "Whether to always reapply freecam customisations when entering freecam."
            );

            usePostProcess = Config.Bind(
                "Freecam", "usePostProcess", true,
                "Whether to use post processing on the freecam."
            );
        }
    }
}
