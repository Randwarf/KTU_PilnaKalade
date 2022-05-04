using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Assets.Scripts.GameCore.Players;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
                                    IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Description;
    public Transform DescriptionPanel;

    public UnityEvent<CardData> onUse = new UnityEvent<CardData>();

    private CanvasGroup CanvasGroup;
    private Canvas canvas;

    private float cardHeigth;
    private float descPanelHeight;

    private CardData cardData;
    private Figure figure;

    private PlayerManager playerManager;

    private void Start() {
        RectTransform descPanelRect = (RectTransform)DescriptionPanel;
        RectTransform cardPanelRect = (RectTransform)transform;
        cardHeigth = cardPanelRect.rect.height;
        descPanelHeight = descPanelRect.rect.height;

        CanvasGroup = GetComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
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

    public void OnBeginDrag(PointerEventData eventData) {
        if (!playerManager.CanPlaceCard(cardData))
        {
            return;
        }

        cardData.figureMap = "111 010 010";
        int[,] figureMap = cardData.GetFigureMap();
        CanvasGroup.DOFade(0f, 0.2f);
        figure = FigureMaker.SpawnFigure(figureMap, FindObjectOfType<Canvas>().transform, Vector2.zero, 100, 5);
        figure.FadeIn();
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!playerManager.CanPlaceCard(cardData))
        {
            return;
        }

        bool successful = figure.PlaceFigure();
        Destroy(figure.gameObject);

        if (successful) {
            onUse.Invoke(cardData);
            Destroy(gameObject);
        }

        CanvasGroup.alpha = 1;
    }

    public void OnDrag(PointerEventData eventData) {
        if (!playerManager.CanPlaceCard(cardData))
        {
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, Input.mousePosition,
            canvas.worldCamera,
            out Vector2 pos);

        figure.transform.localPosition = pos;

        if (figure != null) {
            figure.ShowSelection();
        }
    }
}
