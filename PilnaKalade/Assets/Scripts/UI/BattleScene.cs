using Assets.Scripts.Constants;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    public void OpenSettingsMenu()
    {
        TransitionController.TransitionTo(Scenes.SettingsMenu);
    }

    public void OpenMainMenu()
    {
        TransitionController.TransitionTo(Scenes.MainMenu);
    }
}
