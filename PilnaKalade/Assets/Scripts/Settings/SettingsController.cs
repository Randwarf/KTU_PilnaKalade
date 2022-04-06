using System.IO;
using UnityEngine;

namespace Assets.Scripts.Settings
{
    public static class SettingsController
    {
        public static Settings ActiveSettings { get; private set; }

        private const string GAME_SETTINGS_FILE = "GameSettings.json";
        private static string GetSettingsPath => $"{Application.streamingAssetsPath}/{GAME_SETTINGS_FILE}";
        
        public static void LoadSettings()
        {
            // Do not load settings more than once
            if(ActiveSettings != null)
            {
                return;
            }

            var settingsText = File.ReadAllText(GetSettingsPath);

            ActiveSettings = JsonUtility.FromJson<Settings>(settingsText);

            ApplySettings(ActiveSettings);
        }
        
        public static void UpdateSettings(Settings settings)
        {
            File.WriteAllText(GetSettingsPath, JsonUtility.ToJson(settings));
            
            ApplySettings(settings);
        }

        private static void ApplySettings(Settings settings)
        {
            Screen.fullScreen = settings.IsFullscreen;
            Screen.SetResolution(settings.WindowWidth, settings.WindowHeight, settings.IsFullscreen);

            ActiveSettings = settings;
        }
    }
}
