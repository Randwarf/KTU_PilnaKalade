using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class EncyclopediaCardOld : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Description;
    public Transform DescriptionPanel;

    private float cardHeigth;
    private float descPanelHeight;

    private CardData cardData;

    private void Start() {
        RectTransform descPanelRect = (RectTransform)DescriptionPanel;
        RectTransform cardPanelRect = (RectTransform)transform;
        cardHeigth = cardPanelRect.rect.height;
        descPanelHeight = descPanelRect.rect.height;
    }

    public void SetData(CardData cardData) {
        this.cardData = cardData;
    }

    public void UpdateVisuals() {
        Cost.text = $"Cost: {cardData.cost}";
        Description.text = cardData.description;
    }

    /* Description panel animation */
    public void OnPointerEnter(PointerEventData eventData) {
        DescriptionPanel.DOLocalMoveY(-cardHeigth / 2, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData) {
        DescriptionPanel.DOLocalMoveY(-descPanelHeight - cardHeigth / 2, 0.2f);
    }
    /* */
}
