using Assets.Scripts.UI.Stats;
using UnityEngine;
using System.Linq;

public class StatusSpawner : MonoBehaviour
{
    public GameObject StatusContainer;
    public StatusEffect[] Effects;

    void Start()
    {
        
    }

    public void SpawnEffect(StatusEffectType effectType)
    {
        var effect = Effects.FirstOrDefault(effect => effect.Type == effectType);

        if(effect == null)
        {
            throw new UnityException($"Effect of type: {effectType} does not exist in spawner");
        }

        Instantiate(effect.gameObject, StatusContainer.transform);
    }
}
