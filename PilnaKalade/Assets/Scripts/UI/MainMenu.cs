using Assets.Scripts.Constants;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OpenFightScene() 
    {
        TransitionController.TransitionTo(Scenes.BattleScene);
    }
    public void OpenSettingsMenu()
    {
        TransitionController.TransitionTo(Scenes.SettingsMenu);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}