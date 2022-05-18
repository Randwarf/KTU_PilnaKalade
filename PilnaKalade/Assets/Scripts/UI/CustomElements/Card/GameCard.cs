using Assets.Scripts.GameCore.Players;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameCard : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public UnityEvent<CardData> onUse = new UnityEvent<CardData>();

    [SerializeField] private Transform PrimaryColorPanel;
    [SerializeField] private Text FigureNameText;
    [SerializeField] private Text FigureDescriptionText;
    [SerializeField] private Text CostText;

    public CardData CardData { get; private set; }

    private PlayerManager playerManager;
    private CanvasGroup canvasGroup;
    private Figure figure;
    private Canvas canvas;

    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>();
        playerManager = GameObject.FindWithTag("PlayerManager").GetComponent<PlayerManager>();
    }

    public void SetData(CardData cardData) {
        CardData = cardData;
    }

    public void UpdateVisuals() {
        PlayerDeck.LoadIfUnloaded();
        CardData = PlayerDeck.DrawRandom();

        FigureNameText.text = CardData.title;
        FigureDescriptionText.text = CardData.description;
        CostText.text = "COST: " + CardData.cost;
        int[,] figure = CardData.GetFigureMap();
        FigureMaker.SpawnFigure(figure, PrimaryColorPanel, new Vector2(0, 50), 35, 2).Show();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (!playerManager.CanPlaceCard(CardData)) {
            return;
        }

        int[,] figureMap = CardData.GetFigureMap();
        canvasGroup.DOFade(0f, 0.2f);
        figure = FigureMaker.SpawnFigure(figureMap, FindObjectOfType<Canvas>().transform, Vector2.zero, 110, -3);
        figure.SetCard(this);
        figure.FadeIn();
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (!playerManager.CanPlaceCard(CardData)) {
            return;
        }

        bool successful = figure.PlaceFigure();
        Destroy(figure.gameObject);

        if (successful) {
            onUse?.Invoke(CardData);
            Destroy(gameObject);
        }

        canvasGroup.alpha = 1;
    }

    public void OnDrag(PointerEventData eventData) {
        if (!playerManager.CanPlaceCard(CardData)) {
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

    public void Show() {
        canvasGroup.alpha = 1;
    }

    public void KillTweens() {
        DOTween.Kill(canvasGroup);
    }
}
