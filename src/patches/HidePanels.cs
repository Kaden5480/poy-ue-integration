using System;
using System.Collections.Generic;
using System.Reflection;

using BepInEx.Configuration;
using HarmonyLib;
using UnityExplorer.UI.Panels;
using Panels = UnityExplorer.UI.UIManager.Panels;

namespace UEIntegration.Patches {
    /**
     * <summary>
     * Patches Unity Explorer's panels so they can be
     * shown/hidden by default.
     * </summary>
     */
    internal static class HidePanels {
        private static Dictionary<Panels, ConfigEntry<bool>> configMapping
            = new Dictionary<Panels, ConfigEntry<bool>> {
            { Panels.ObjectExplorer, Config.showObjectExplorer },
            { Panels.Inspector,      Config.showInspector      },
            { Panels.CSConsole,      Config.showConsole        },
            { Panels.HookManager,    Config.showHooks          },
            { Panels.Freecam,        Config.showFreecam        },
            { Panels.Clipboard,      Config.showClipboard      },
            { Panels.ConsoleLog,     Config.showLog            },
            { Panels.Options,        Config.showOptions        },
        };

        /**
         * <summary>
         * Enables/disables panels by default based upon
         * user settings.
         * </summary>
         */
        [HarmonyPatch(typeof(UEPanel), "ConstructUI")]
        private static void Postfix(UEPanel __instance) {
            if (configMapping.TryGetValue(
                    __instance.PanelType, out ConfigEntry<bool> entry
            ) == false) {
                return;
            }

            __instance.SetActive(entry.Value);
        }
    }
}
