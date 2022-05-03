using Assets.Scripts.Constants;
using Assets.Scripts.UI.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameCore.Players
{
    public class PlayerManager : MonoBehaviour
    {
        private Player _player;
        private Player _enemyPlayer;

        private bool _playerTurn;

        private UIManager _uiManager;

        private CardManager _cardManager;

        void Awake()
        {
            _playerTurn = true;

             GameObject.FindWithTag("NextTurnButton")
                .GetComponent<Button>()
                .onClick
                .AddListener(ConfirmState);

            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _enemyPlayer = GameObject.FindWithTag("Enemy").GetComponent<Player>();

            _uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();

            _cardManager = GameObject.FindWithTag("CardManager").GetComponent<CardManager>();
            _cardManager.OnCardEndUse.AddListener(UpdateState);

            _uiManager.InitBarValues(_player.Defense, _player.Health, _player.Mana, true);
            _uiManager.InitBarValues(_enemyPlayer.Defense, _enemyPlayer.Health, _enemyPlayer.Mana, false);
        }

        private void UpdateState(CardData cardData)
        {
            var player = GetPlayer(_playerTurn);
            var damage = Game.DefaultDamage * cardData.stats.damagemultiplier;
            var difference = player.Defense - damage;

            player.Defense -= damage;

            if(difference < 0)
            {
                player.Health += difference;
            }

            _uiManager.ShowPredictionDamagePoints(damage, _playerTurn);
            _uiManager.ShowPredictionManaPoints(cardData.cost, _playerTurn);
        }

        private void ConfirmState()
        {
            _uiManager.ConfirmPredictionPoints(_playerTurn);

            // Could put enemy behaviour logic here
            // Temporary demo
            _playerTurn = !_playerTurn; // End player turn, start enemy turn
            
            var enemy = GetPlayer(_playerTurn);
            var randomDamage = 30;
            enemy.Defense -= randomDamage;
            
            if(enemy.Defense <= randomDamage)
            {
                enemy.Health -= randomDamage;
            }
            _uiManager.ShowPredictionDamagePoints(randomDamage, _playerTurn);
            _uiManager.ConfirmPredictionPoints(_playerTurn);

            _playerTurn = !_playerTurn; // End enemy turn, start player turn
        }

        private Player GetPlayer(bool playerTurn)
        {
            return playerTurn ? _enemyPlayer : _player;
        }
    }
}
