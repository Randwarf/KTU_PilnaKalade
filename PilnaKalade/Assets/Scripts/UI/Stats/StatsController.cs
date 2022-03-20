using Assets.Scripts.UI.Stats;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public StatusSpawner Spawner;

    public Bar HealthBar;
    public Bar ManaBar;

    void Start()
    {
        // Mock values for demonstration purposes
        HealthBar.SetMaxValue(100);
        HealthBar.SetValue(42);

        ManaBar.SetMaxValue(100);
        ManaBar.SetValue(30);

        Spawner.SpawnEffect(StatusEffectType.Mana);
        Spawner.SpawnEffect(StatusEffectType.Health);
        Spawner.SpawnEffect(StatusEffectType.Mana);
        Spawner.SpawnEffect(StatusEffectType.Health);
        Spawner.SpawnEffect(StatusEffectType.Mana);
        Spawner.SpawnEffect(StatusEffectType.Health);
    }
}
