using Assets.Scripts.UI.Stats;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    public StatusSpawner Spawner;

    public Bar DefenseBar;
    public Bar HealthBar;
    public Bar ManaBar;

    public void SpawnEffect(StatusEffectType type)
    {
        Spawner.SpawnEffect(type);
    }

    public void SetMaxBarValue(int maxValue, BarType type)
    {
        GetBar(type).MaxValue = maxValue;
    }

    public void SetBarValue(int value, BarType type)
    {
        GetBar(type).Value = value;
    }

    public void IncreaseBarValue(int value, BarType type)
    {
        GetBar(type).Value += value;
    }

    public void DecreaseBarValue(int value, BarType type)
    {
        GetBar(type).Value -= value;
    }

    private Bar GetBar(BarType type)
    {
        return type switch
        {
            BarType.Defense => DefenseBar,
            BarType.Health => HealthBar,
            BarType.Mana => ManaBar,
            _ => null,
        };
    }
}
