using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScene : MonoBehaviour
{
    public void OpenSettingsMenu()
    {
        TransitionController.TransitionTo(2);
    }

}
