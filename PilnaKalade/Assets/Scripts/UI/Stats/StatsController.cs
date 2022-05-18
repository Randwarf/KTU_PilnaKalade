using Assets.Scripts.UI.Stats;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    //public StatusSpawner Spawner;

    public Bar DefenseBar;
    public Bar HealthBar;
    public Bar ManaBar;

    //public void SpawnEffect(StatusEffectType type)
    //{
    //    Spawner.SpawnEffect(type);
    //}

    public void ConfirmPredictionPoints()
    {
        DefenseBar.CompleteSynchronization();
        HealthBar.CompleteSynchronization();
        ManaBar.CompleteSynchronization();
    }

    public void CancelPredictionPoints()
    {
        DefenseBar.CancelSynchronization();
        HealthBar.CancelSynchronization();
        ManaBar.CancelSynchronization();
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

    public void SetManaPredictionPoints(int potentialNextTurnManaPoints)
    {
        DecreaseBarValue(potentialNextTurnManaPoints, BarType.Mana);
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
