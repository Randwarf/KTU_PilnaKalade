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

    private void Update()
    {
        if(_childrenCount > StatusContainer.childCount)
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

        if(StatusContainer.childCount == 1) // StatusSpawner is container's child
        {
            var firstEffect = Instantiate(effectPrefab, StatusContainer.position, Quaternion.identity);
            firstEffect.transform.SetParent(StatusContainer, false);
            firstEffect.transform.position = transform.position;
            
            _childrenCount = StatusContainer.childCount;
            
            return;
        }
        
        var prevChild = StatusContainer.GetChild(StatusContainer.childCount - 1);

        var effect = Instantiate(effectPrefab);
        effect.transform.SetParent(StatusContainer, false);
        effect.GetComponent<RectTransform>().anchoredPosition = GetNextAnchoredPositionFromChild(prevChild);

        _childrenCount = StatusContainer.childCount;
    }
    
    private void RecalculateChildrenPositions()
    {
        Transform prevChild = null;

        for (var i = 1; i < StatusContainer.childCount; i++)
        {
            var child = StatusContainer.GetChild(i);

            if (prevChild == null)
            {
                child.position = transform.position;
            }
            else
            {
                child.GetComponent<RectTransform>().anchoredPosition = GetNextAnchoredPositionFromChild(prevChild);
            }

            prevChild = child;
        }

        _childrenCount = StatusContainer.childCount;
    }

    private Vector2 GetNextAnchoredPositionFromChild(Transform child)
    {
        var prevChildRectTransform = child.GetComponent<RectTransform>();

        return new Vector2(prevChildRectTransform.anchoredPosition.x + prevChildRectTransform.rect.width + RightPadding,
            prevChildRectTransform.anchoredPosition.y);
    }
}
