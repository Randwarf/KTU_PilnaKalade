using UnityEngine;

namespace Assets.Scripts.UI.Stats
{
    public class UIManager : MonoBehaviour
    {
        public StatsController EnemyStatsController;
        public StatsController PlayerStatsController;

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

        public void ShowPredictionDamagePoints(int potentialNextTurnDamagePoints, bool playerTurn)
        {
            GetCurrentStatsController(playerTurn).SetDamagePredictionPoints(potentialNextTurnDamagePoints);
        }

        public void ConfirmDamagePredictionPoints(bool playerTurn)
        {
            GetCurrentStatsController(playerTurn).ConfirmDamagePredictionPoints();
        }

        public void CancelDamagePredictionPoints(bool playerTurn)
        {
            GetCurrentStatsController(playerTurn).CancelDamagePredictionPoints();
        }

        private StatsController GetCurrentStatsController(bool playerTurn)
        {
            if (playerTurn)
            {
                return EnemyStatsController;
            }

            return PlayerStatsController;
        }
    }
}
