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
    private CanvasGroup CanvasGroup;
    private Canvas canvas;
    private float cardHeigth;
    private float descPanelHeight;
    private Figure figure;
    
    private PlayerManager playerManager;
    private CardData cardData;

    public UnityEvent<CardData> onUse = new UnityEvent<CardData>();

    private void Start() {
        RectTransform descPanelRect = (RectTransform)DescriptionPanel;
        RectTransform cardPanelRect = (RectTransform)transform;
        cardHeigth = cardPanelRect.rect.height;
        descPanelHeight = descPanelRect.rect.height;

        CanvasGroup = GetComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();

        PlayerDeck.LoadIfUnloaded();
        cardData = PlayerDeck.DrawRandom();

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

        int[,] figureMap = cardData.GetFigureMap();
        CanvasGroup.DOFade(0.0f, 0.2f);
        figure = FigureMaker.SpawnFigure(figureMap, FindObjectOfType<Canvas>(), Vector2.zero);
        figure.FadeIn();
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!playerManager.CanPlaceCard(cardData))
        {
            return;
        }

        CanvasGroup.DOKill();
        CanvasGroup.alpha = 1;

        bool successful = figure.PlaceFigure();
        Destroy(figure.gameObject);

        if (successful) {
            onUse.Invoke(cardData);
            Destroy(gameObject);
        }
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

        if(figure != null) {
            figure.transform.localPosition = pos;
            figure.ShowSelection();
        }
    }
}
