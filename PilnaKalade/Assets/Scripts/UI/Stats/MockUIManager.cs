using UnityEngine;

namespace Assets.Scripts.UI.Stats
{
    public class MockUIManager : MonoBehaviour
    {
        public StatsController EnemyStatsController;
        public StatsController PlayerStatsController;

        // Sample values for demonstration
        private void Start()
        {
            PlayerStatsController.SetMaxBarValue(100, BarType.Defense);
            PlayerStatsController.SetMaxBarValue(100, BarType.Health);
            PlayerStatsController.SetMaxBarValue(100, BarType.Mana);
            PlayerStatsController.DecreaseBarValue(100000000, BarType.Mana);
            PlayerStatsController.IncreaseBarValue(30, BarType.Mana);
            PlayerStatsController.DecreaseBarValue(100, BarType.Defense);
            PlayerStatsController.SpawnEffect(StatusEffectType.Mana);
            PlayerStatsController.SpawnEffect(StatusEffectType.Health);
            PlayerStatsController.SpawnEffect(StatusEffectType.Mana);

            EnemyStatsController.SetMaxBarValue(100, BarType.Defense);
            EnemyStatsController.SetMaxBarValue(100, BarType.Health);
            EnemyStatsController.SetMaxBarValue(100, BarType.Mana);
            EnemyStatsController.DecreaseBarValue(66, BarType.Defense);
            EnemyStatsController.SpawnEffect(StatusEffectType.Mana);
        }
    }
}
