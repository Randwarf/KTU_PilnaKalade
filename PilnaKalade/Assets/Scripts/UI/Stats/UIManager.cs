using UnityEngine;

namespace Assets.Scripts.UI.Stats
{
    public class UIManager : MonoBehaviour
    {
        public StatsController EnemyStatsController;
        public StatsController PlayerStatsController;
        public GameObject VictoryScreen;
        public GameObject DefeatScreen;

        public void InitBarValues(int maxDefenseValue, int maxHealthValue, int maxManaValue, bool updatePlayer)
        {
            if(!updatePlayer)
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

        public void SpawnPoisonEffect(StatusEffectType type)
        {
            //EnemyStatsController.SpawnEffect(type);
        }

        public void ShowPredictionDamagePoints(int potentialNextTurnDamagePoints, bool updatePlayer)
        {
            GetCurrentStatsController(updatePlayer).SetDamagePredictionPoints(potentialNextTurnDamagePoints);
        }

        public void ShowPredictionManaPoints(int potentialNextTurnManaPoints, bool updatePlayer)
        {
            GetCurrentStatsController(updatePlayer).SetManaPredictionPoints(potentialNextTurnManaPoints);
        }

        public void ConfirmPredictionPoints(bool updatePlayer)
        {
            GetCurrentStatsController(updatePlayer).ConfirmPredictionPoints();
        }

        public void CancelPredictionPoints(bool updatePlayer)
        {
            GetCurrentStatsController(updatePlayer).CancelPredictionPoints();
        }

        public void ClearEnemyStatusEffects()
        {
            EnemyStatsController.ClearEffects();
        }

        private StatsController GetCurrentStatsController(bool updatePlayer)
        {
            if (!updatePlayer)
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
