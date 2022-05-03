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

    public void ConfirmDamagePredictionPoints()
    {
        DefenseBar.CompleteSynchronization();
        HealthBar.CompleteSynchronization();
    }

    public void CancelDamagePredictionPoints()
    {
        DefenseBar.CancelSynchronization();
        HealthBar.CancelSynchronization();
    }

    public void SetDamagePredictionPoints(int potentialNextTurnDamagePoints)
    {
        var difference = potentialNextTurnDamagePoints - DefenseBar.Value;
        
        if (difference > 0)
        {
            DecreaseBarValue(difference, BarType.Health);
        }

        DecreaseBarValue(potentialNextTurnDamagePoints, BarType.Defense);
    }

    public void SetMaxBarValue(int maxValue, BarType type)
    {
        GetBar(type).MaxValue = maxValue;
    }
    
    private void IncreaseBarValue(int value, BarType type)
    {
        GetBar(type).Value += value;
    }

    private void DecreaseBarValue(int value, BarType type)
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
