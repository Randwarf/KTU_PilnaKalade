using Assets.Scripts.Constants;
using Assets.Scripts.UI.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameCore.Players
{
    public class PlayerManager : MonoBehaviour
    {
        private const bool UpdateEnemy = false;
        private const bool UpdatePlayer = true;

        private Player _player;
        private Player _enemyPlayer;

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
            DoDamageToEnemy(cardData);
            DecreasePlayerMana(cardData);
        }

        public bool CanPlaceCard(CardData cardData)
        {
            return _player.Mana >= cardData.cost;
        }

        private void OnExhaustClearPoison()
        {
            _enemyPlayer.ClearPoison();
            _uiManager.ClearEnemyStatusEffects();
        }

        private void OnNextTurnConfirmState()
        {
            // Update stats for enemy
            _uiManager.ConfirmPredictionPoints(UpdateEnemy);

            if(_enemyPlayer.Health <= 0)
            {
                _uiManager.Victory();
                return;
            }

            DoDamageToPlayer(Random.Range(10, 50));

            if (_player.Health <= 0)
            {
                _uiManager.Defeat();
                return;
            }

            ResetPlayerMana();

            // Update stats for enemy
            _uiManager.ShowPredictionDamagePoints(_enemyPlayer.ApplyPoisonDamage(), UpdateEnemy);
        }

        private void ResetPlayerMana()
        {
            _player.Mana = _previousMana;
            _uiManager.InitPlayerBarValue(_previousMana, BarType.Mana);
        }

        private void InitPlayers()
        {
            _player.Defense = Game.DefaultEnemyDefense;
            _player.Health = Game.DefaultEnemyHealth;
            _player.Mana = Game.DefaultPlayerMana;
            _enemyPlayer.Defense = Game.DefaultPlayerDefense;
            _enemyPlayer.Health = Game.DefaultPlayerHealth;

            _previousMana = _player.Mana;

            _uiManager.InitBarValues(_player.Defense, _player.Health, _player.Mana, UpdatePlayer);
            _uiManager.InitBarValues(_enemyPlayer.Defense, _enemyPlayer.Health, _enemyPlayer.Mana, UpdateEnemy);
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
                .AddListener(OnNextTurnConfirmState);

            GameObject.FindWithTag("ExhaustButton")
                .GetComponent<Button>()
                .onClick
                .AddListener(OnExhaustClearPoison);

            _cardManager.OnCardEndUse.AddListener(UpdateState);
        }

        private void IfExistsAddPoisonToEnemy(CardData cardData)
        {
            if (cardData.stats.poisonDamagePerTurn == 0)
            {
                return;
            }

            _enemyPlayer.AddPoison(cardData.stats.poisonDamagePerTurn);
            _enemyPlayer.takeDamage(cardData.stats.poisonDamagePerTurn);

            SpawnPoison(cardData.stats.poisonDamagePerTurn);
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

        private void DoDamageToEnemy(CardData cardData)
        {
            IfExistsAddPoisonToEnemy(cardData);

            var damage = Game.DefaultDamage * cardData.stats.damagemultiplier;
            _enemyPlayer.takeDamage(damage);

            _uiManager.ShowPredictionDamagePoints(damage, UpdateEnemy);
            _uiManager.ShowPredictionManaPoints(cardData.cost, UpdateEnemy);
        }

        private void DoDamageToPlayer(int damage)
        {
            _player.takeDamage(damage);

            _uiManager.ShowPredictionDamagePoints(damage, UpdatePlayer);
            _uiManager.ConfirmPredictionPoints(UpdatePlayer);
        }

        private void DecreasePlayerMana(CardData cardData)
        {
            _player.Mana -= cardData.cost;
        }
    }
}
