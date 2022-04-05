using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OpenFightScene() {
        TransitionController.TransitionTo(1);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TransitionController.TransitionTo(2);
        }
    }
}