using Assets.Scripts.Constants;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    public void OpenMainMenu()
    {
        TransitionController.TransitionTo(Scenes.MainMenu);
    }
}
