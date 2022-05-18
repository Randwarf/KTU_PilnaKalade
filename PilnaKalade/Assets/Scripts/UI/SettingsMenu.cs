using Assets.Scripts.Constants;
using Assets.Scripts.Settings;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    //public Toggle FullscreenToggle;
    public CustomCheckbox FullscreenToggle;

    //public Dropdown ResolutionDropdown;
    public CustomDropdown ResolutionDropdown;

    private Settings _activeSettings;
    private bool _toggleEmittedFirstEvent;

    private void Awake()
    {
        InitSettings();
        InitToggle();
        InitResolutionDropdown();
    }

    public void OpenMainMenuScene()
    {
        TransitionController.TransitionTo(Scenes.MainMenu);
    }

    public void ToggleFullscreen()
    {
        // Have to interrupt first call to this function, because toggle emits unnecessary onValueChange event on initialization
        if (!_toggleEmittedFirstEvent)
        {
            _toggleEmittedFirstEvent = true;
            return;
        }

        _activeSettings.IsFullscreen = !_activeSettings.IsFullscreen;

        SettingsController.UpdateSettings(_activeSettings);
    }

    public void ChangeResolution()
    {
        var resolutionText = ResolutionDropdown.SelectedOptionText;
        var parts = resolutionText.Split(' ');
        
        var newWidth = Convert.ToInt32(parts[0]);
        var newHeight = Convert.ToInt32(parts[2]);

        _activeSettings.WindowWidth = newWidth;
        _activeSettings.WindowHeight = newHeight;

        SettingsController.UpdateSettings(_activeSettings);
    }

    private void InitSettings()
    {
        // Make sure settings are loaded (only matters when running this scene directly)
        SettingsController.LoadSettings();

        _activeSettings = SettingsController.ActiveSettings;
    }

    private void InitToggle()
    {
        if(FullscreenToggle.isOn == _activeSettings.IsFullscreen)
        {
            _toggleEmittedFirstEvent = true;
        }
        else
        {
            _toggleEmittedFirstEvent = false;
        }

        FullscreenToggle.SetToggle(_activeSettings.IsFullscreen);
    }

    private void InitResolutionDropdown()
    {
        var resolutionString = $"{_activeSettings.WindowWidth} x {_activeSettings.WindowHeight}";

        ResolutionDropdown.Interactable = false;
        ResolutionDropdown.SetSelectedIndex(ResolutionDropdown.options.FindIndex(option => option == resolutionString));
        ResolutionDropdown.Interactable = true;
    }
}