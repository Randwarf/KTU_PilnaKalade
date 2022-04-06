using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OpenFightScene() 
    {
        TransitionController.TransitionTo(1);
    }
    
    public void OpenMenuScene()
    {
        TransitionController.TransitionTo(2);
    }
    
    public void ExitGame() 
    {
        Application.Quit();
    }
}