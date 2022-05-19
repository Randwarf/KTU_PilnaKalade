using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverButton : MonoBehaviour
{
    private BattleScene sceneScript;

    // Start is called before the first frame update
    void Start()
    {
        GameObject mcanvas = GameObject.Find("MainCanvas");
        sceneScript = mcanvas.GetComponent<BattleScene>();
    }

    public void Leave()
    {
        PlayerDeck.Clear();
        if (PlayerPrefs.HasKey("Deck"))
        {
            PlayerPrefs.DeleteKey("Deck");
            PlayerPrefs.Save();
        }
        sceneScript.OpenMainMenu();
    }

    public void Continue()
    {
        sceneScript.OpenMainMenu();
        //var obj = GameObject.Find("VictoryScreen(Clone)");
        //GameObject.Destroy(obj);
        //sceneScript.Reload();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
