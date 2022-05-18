using Assets.Scripts.UI.Stats;
using UnityEngine;
using System.Linq;

public class StatusSpawner : MonoBehaviour
{
    public Transform StatusContainer;
    public StatusEffect[] Effects;

    public int RightPadding;

    private int _childrenCount;

    void Start()
    {
        _childrenCount = StatusContainer.childCount;
    }

    public void ClearEffects()
    {
        for (var i = 1; i < StatusContainer.childCount; i++)
        {
            Destroy(StatusContainer.GetChild(i).gameObject);
        }
    }

    public void SpawnEffect(StatusEffectType effectType)
    {
        var effectPrefab = Effects.FirstOrDefault(effect => effect.Type == effectType);

        if (effectPrefab == null)
        {
            throw new UnityException($"Effect of type: {effectType} does not exist in spawner");
        }

        if(StatusContainer.childCount == 1) // StatusSpawner is container's child
        {
            var firstEffect = Instantiate(effectPrefab);
            firstEffect.transform.SetParent(StatusContainer, false);
            firstEffect.GetComponent<RectTransform>().localPosition = GetNextAnchoredPositionFromChild(firstEffect.transform);
            _childrenCount = StatusContainer.childCount;
            
            return;
        }
        
        var prevChild = StatusContainer.GetChild(StatusContainer.childCount - 1);

        var effect = Instantiate(effectPrefab);
        effect.transform.SetParent(StatusContainer, false);
        effect.GetComponent<RectTransform>().localPosition = GetNextAnchoredPositionFromChild(prevChild);

        _childrenCount = StatusContainer.childCount;
    }

    private Vector2 GetNextAnchoredPositionFromChild(Transform child)
    {
        var prevChildRectTransform = child.GetComponent<RectTransform>();

        return new Vector2(prevChildRectTransform.localPosition.x + prevChildRectTransform.rect.width + RightPadding,
            prevChildRectTransform.localPosition.y);
    }
}
