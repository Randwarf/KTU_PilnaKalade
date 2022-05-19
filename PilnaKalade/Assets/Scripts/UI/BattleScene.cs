using Assets.Scripts.Constants;
using UnityEngine;
using Assets.Scripts.GameCore.Players;

public class BattleScene : MonoBehaviour
{
    public void OpenMainMenu()
    {
        TransitionController.TransitionTo(Scenes.MainMenu);
    }

    public void Reload()
    {
        var deck = PlayerDeck.GetDeck();
        var playerManager = GameObject.Find("PlayerManager");
        playerManager.GetComponent<PlayerManager>().Start();
        PlayerDeck.SetDeck(deck);
        var grid = GameObject.Find("Grid");
        var gridComponent = grid.GetComponent<GameGrid>();
        gridComponent.ClearTiles();
        
        //TransitionController.TransitionTo(Scenes.BattleScene);
    }

    private void Start()
    {
        EnemyDatabase.LoadEnemies();
    }
}
