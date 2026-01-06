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

        // Panels
        [Field("Object Explorer")]
        internal static ConfigEntry<bool> showObjectExplorer { get; private set; }

        [Field("Inspector")]
        internal static ConfigEntry<bool> showInspector { get; private set; }

        [Field("C# Console")]
        internal static ConfigEntry<bool> showConsole { get; private set; }

        [Field("Hooks")]
        internal static ConfigEntry<bool> showHooks { get; private set; }

        [Field("Freecam")]
        internal static ConfigEntry<bool> showFreecam { get; private set; }

        [Field("Clipboard")]
        internal static ConfigEntry<bool> showClipboard { get; private set; }

        [Field("Log")]
        internal static ConfigEntry<bool> showLog { get; private set; }

        [Field("Options")]
        internal static ConfigEntry<bool> showOptions { get; private set; }

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

            // Panels
            showObjectExplorer = configFile.Bind(
                "Panels", "showObjectExplorer", false,
                "Whether to show the object explorer panel by default."
            );

            showInspector = configFile.Bind(
                "Panels", "showInspector", false,
                "Whether to show the inspector panel by default."
            );

            showConsole = configFile.Bind(
                "Panels", "showConsole", false,
                "Whether to show the console panel by default."
            );

            showHooks = configFile.Bind(
                "Panels", "showHooks", false,
                "Whether to show the hooks panel by default."
            );

            showFreecam = configFile.Bind(
                "Panels", "showFreecam", false,
                "Whether to show the freecam panel by default."
            );

            showClipboard = configFile.Bind(
                "Panels", "showClipboard", false,
                "Whether to show the clipboard panel by default."
            );

            showLog = configFile.Bind(
                "Panels", "showLog", false,
                "Whether to show the log panel by default."
            );

            showOptions = configFile.Bind(
                "Panels", "showOptions", false,
                "Whether to show the options panel by default."
            );
        }
    }
}
