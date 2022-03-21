using Assets.Scripts.UI.Stats;
using UnityEngine;
using System.Linq;

public class StatusSpawner : MonoBehaviour
{
    public StatusEffect[] Effects;
    public int RightPadding;


    private int _childrenCount;

    void Start()
    {
        _childrenCount = transform.childCount;
    }

    private void Update()
    {
        if(_childrenCount > transform.childCount)
        {
            RecalculateChildrenPositions();
        }
    }

    public void SpawnEffect(StatusEffectType effectType)
    {
        var effectPrefab = Effects.FirstOrDefault(effect => effect.Type == effectType);

        if (effectPrefab == null)
        {
            throw new UnityException($"Effect of type: {effectType} does not exist in spawner");
        }

        var nextPosition = transform.position;

        if(transform.childCount != 0)
        {
            var lastChild = transform.GetChild(transform.childCount - 1);

            nextPosition = new Vector3(lastChild.transform.position.x + ((RectTransform)lastChild.transform).rect.width + RightPadding,
                lastChild.position.y);
        }
        
        var effect = Instantiate(effectPrefab, nextPosition, Quaternion.identity);
        effect.transform.SetParent(transform);

        _childrenCount = transform.childCount;
    }
    
    private void RecalculateChildrenPositions()
    {
        Transform prevChild = null;

        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (prevChild != null)
            {
                var nextPos = new Vector3(prevChild.transform.position.x + ((RectTransform)prevChild.transform).rect.width + RightPadding,
                    prevChild.transform.position.y);

                child.position = nextPos;
            }
            else
            {
                child.position = transform.position;
            }

            prevChild = child;
        }

        _childrenCount = transform.childCount;
    }
}
