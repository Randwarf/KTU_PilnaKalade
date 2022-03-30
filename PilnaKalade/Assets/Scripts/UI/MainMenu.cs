using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OpenFightScene() {
        TransitionController.TransitionTo(1);
    }
}