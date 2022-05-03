using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Settings;
using System;

public class BattleSettings : MonoBehaviour
{
    public CanvasGroup CanvasGroup;

    public Toggle FullscreenToggle;

    public Dropdown ResolutionDropdown;

    private Settings _activeSettings;
    private bool _toggleEmittedFirstEvent;

    private void Awake()
    {
        InitSettings();
        InitToggle();
        InitResolutionDropdown();
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
        var resolutionText = ResolutionDropdown.options[ResolutionDropdown.value].text;
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
        if (FullscreenToggle.isOn == _activeSettings.IsFullscreen)
        {
            _toggleEmittedFirstEvent = true;
        }
        else
        {
            _toggleEmittedFirstEvent = false;
        }

        FullscreenToggle.isOn = _activeSettings.IsFullscreen;
    }

    private void InitResolutionDropdown()
    {
        var resolutionString = $"{_activeSettings.WindowWidth} x {_activeSettings.WindowHeight}";

        ResolutionDropdown.interactable = false;
        ResolutionDropdown.value = ResolutionDropdown.options.FindIndex(option => option.text == resolutionString);
        ResolutionDropdown.interactable = true;
    }

    public void Open()
    {
        gameObject.SetActive(true);
        CanvasGroup.DOFade(1f, 0.2f);
        Awake();
    }

    public void Close()
    {
        CanvasGroup.DOFade(0f, 0.2f).OnComplete(() => gameObject.SetActive(false));
    }
}
