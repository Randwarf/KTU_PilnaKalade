using Assets.Scripts.UI.Stats;
using System.Collections;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public StatusSpawner Spawner;

    public Bar DefenseBar;
    public Bar HealthBar;
    public Bar ManaBar;

    void Start()
    {
        // Mock values for demonstration purposes
        HealthBar.SetMaxValue(100);
        HealthBar.SetValue(42);

        ManaBar.SetMaxValue(100);
        ManaBar.SetValue(30);

        DefenseBar.SetMaxValue(100);

        Spawner.SpawnEffect(StatusEffectType.Mana);
        Spawner.SpawnEffect(StatusEffectType.Health);

        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(1);

        Spawner.SpawnEffect(StatusEffectType.Mana);
        Spawner.SpawnEffect(StatusEffectType.Health);
    }
}
