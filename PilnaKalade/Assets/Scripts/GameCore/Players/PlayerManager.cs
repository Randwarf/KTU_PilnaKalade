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
        private int _previousMana;

        private UIManager _uiManager;

        private CardManager _cardManager;

        void Start()
        {

            GetComponents();
            InitPlayers();
            AddListeners();
        }

        private void UpdateState(CardData cardData)
        {
            var damage = Game.DefaultDamage * cardData.stats.damagemultiplier;


            IfExistsAddPoisonToEnemy(cardData);

            _enemyPlayer.takeDamage(damage);

            _uiManager.ShowPredictionDamagePoints(damage, _playerTurn);
            _uiManager.ShowPredictionManaPoints(cardData.cost, _playerTurn);

            _player.Mana -= cardData.cost;
        }

        private void ConfirmState()
        {
            _uiManager.ConfirmPredictionPoints(_playerTurn);

            // Could put enemy behaviour logic here
            // Temporary demo
            _playerTurn = !_playerTurn; // End player turn, start enemy turn
            
            var randomDamage = Random.Range(10, 50);
            _player.takeDamage(randomDamage);

            _uiManager.ShowPredictionDamagePoints(randomDamage, _playerTurn);
            _uiManager.ConfirmPredictionPoints(_playerTurn);

            _playerTurn = !_playerTurn; // End enemy turn, start player turn

            //check if enemy died
            if (_enemyPlayer.Health <= 0 && _player.Health > 0)
            {
                _uiManager.Victory();
            }

            //check if player died
            if (_player.Health <= 0)
            {
                _uiManager.Defeat();
            }

            // Reseting mana
            _player.Mana = _previousMana;

            _uiManager.InitPlayerBarValue(_previousMana, BarType.Mana);

            _uiManager.ShowPredictionDamagePoints(_enemyPlayer.ApplyPoisonDamage(), _playerTurn);
        }

        public bool CanPlaceCard(CardData cardData)
        {
            return _player.Mana >= cardData.cost;
        }

        public void ClearPoison()
        {
            _enemyPlayer.ClearPoison();
            _uiManager.ClearEnemyStatusEffects();
        }

        private void InitPlayers()
        {
            _playerTurn = true;

            _player.Defense = Game.DefaultEnemyDefense;
            _player.Health = Game.DefaultEnemyHealth;
            _player.Mana = Game.DefaultPlayerMana;
            _enemyPlayer.Defense = Game.DefaultPlayerDefense;
            _enemyPlayer.Health = Game.DefaultPlayerHealth;

            _previousMana = _player.Mana;

            _uiManager.InitBarValues(_player.Defense, _player.Health, _player.Mana, true);
            _uiManager.InitBarValues(_enemyPlayer.Defense, _enemyPlayer.Health, _enemyPlayer.Mana, false);
        }

        private void GetComponents()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<Player>();
            _enemyPlayer = GameObject.FindWithTag("Enemy").GetComponent<Player>();
            _uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
            _cardManager = GameObject.FindWithTag("CardManager").GetComponent<CardManager>();
        }

        private void AddListeners()
        {
            GameObject.FindWithTag("NextTurnButton")
                .GetComponent<Button>()
                .onClick
                .AddListener(ConfirmState);

            GameObject.FindWithTag("ExhaustButton")
                .GetComponent<Button>()
                .onClick
                .AddListener(ClearPoison);

            _cardManager.OnCardEndUse.AddListener(UpdateState);
        }

        private void IfExistsAddPoisonToEnemy(CardData cardData)
        {
            if (cardData.stats.poisonDamagePerTurn != 0)
            {
                _enemyPlayer.AddPoison(cardData.stats.poisonDamagePerTurn);
                _enemyPlayer.takeDamage(cardData.stats.poisonDamagePerTurn);

                SpawnPoison(cardData.stats.poisonDamagePerTurn);
            }
        }

        private void SpawnPoison(int poisonDamagePoints)
        {
            if (poisonDamagePoints > 10)
            {
                _uiManager.SpawnPoisonEffect(StatusEffectType.PoisonMedium);
            }
            else
            {
                _uiManager.SpawnPoisonEffect(StatusEffectType.PoisonLow);
            }
        }
    }
}
