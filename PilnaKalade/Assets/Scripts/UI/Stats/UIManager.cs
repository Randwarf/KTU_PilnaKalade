using UnityEngine;

namespace Assets.Scripts.UI.Stats
{
    public class UIManager : MonoBehaviour
    {
        public StatsController EnemyStatsController;
        public StatsController PlayerStatsController;
        public GameObject VictoryScreen;
        public GameObject DefeatScreen;

        public void InitBarValues(int maxDefenseValue, int maxHealthValue, int maxManaValue, bool forPlayer)
        {
            if(!forPlayer)
            {
                EnemyStatsController.SetMaxBarValue(maxDefenseValue, BarType.Defense);
                EnemyStatsController.SetMaxBarValue(maxHealthValue, BarType.Health);
                EnemyStatsController.SetMaxBarValue(maxManaValue, BarType.Mana);

                return;
            }

            PlayerStatsController.SetMaxBarValue(maxDefenseValue, BarType.Defense);
            PlayerStatsController.SetMaxBarValue(maxHealthValue, BarType.Health);
            PlayerStatsController.SetMaxBarValue(maxManaValue, BarType.Mana);
        }

        public void InitPlayerBarValue(int maxValue, BarType type)
        {
            PlayerStatsController.SetMaxBarValue(maxValue, type);
        }

        public void ShowPredictionDamagePoints(int potentialNextTurnDamagePoints, bool playerTurn)
        {
            GetCurrentStatsController(playerTurn).SetDamagePredictionPoints(potentialNextTurnDamagePoints);
        }

        public void ShowPredictionManaPoints(int potentialNextTurnManaPoints, bool playerTurn)
        {
            GetCurrentStatsController(!playerTurn).SetManaPredictionPoints(potentialNextTurnManaPoints);
        }

        public void ConfirmPredictionPoints(bool playerTurn)
        {
            GetCurrentStatsController(playerTurn).ConfirmPredictionPoints();
        }

        public void CancelPredictionPoints(bool playerTurn)
        {
            GetCurrentStatsController(playerTurn).CancelPredictionPoints();
        }

        private StatsController GetCurrentStatsController(bool playerTurn)
        {
            if (playerTurn)
            {
                return EnemyStatsController;
            }

            return PlayerStatsController;
        }

        public void Victory()
        {
            GameObject.Instantiate(VictoryScreen);
        }
        public void Defeat()
        {
            GameObject.Instantiate(DefeatScreen);
        }
    }
}
