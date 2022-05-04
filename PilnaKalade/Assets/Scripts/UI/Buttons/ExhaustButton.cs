using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExhaustButton : MonoBehaviour
{
    public float MinProportionRequired = 0.5f;

    private GameGrid gameGrid;
    private CardManager cardManager;
    private CustomButton exhaustButton;
    private CanvasGroup buttonCanvasGroup;

    private bool IsExhaustEnabled()
    {
        return gameGrid.GetPlacedTilesProportion() > MinProportionRequired 
            && cardManager.NoCardsDrawnAfterDiscard;
    }

    void Awake()
    {
        gameGrid = GameObject.FindWithTag("Grid").GetComponent<GameGrid>();
        cardManager = GameObject.FindWithTag("CardManager").GetComponent<CardManager>();
        exhaustButton = GetComponent<CustomButton>();
        buttonCanvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        gameGrid.onTilePlacementChanges.AddListener(UpdateButton);
        cardManager.onNoCardsDrawnAfterDiscardChange.AddListener(UpdateButton);
        exhaustButton.OnPressed.AddListener(OnExhaustClick);
        UpdateButton();
    }

    void OnDisable()
    {
        gameGrid.onTilePlacementChanges.RemoveListener(UpdateButton);
        cardManager.onNoCardsDrawnAfterDiscardChange.RemoveListener(UpdateButton);
        exhaustButton.OnPressed.RemoveListener(OnExhaustClick);
    }

    void OnValidate()
    {
        //OnValidate executes when inspector properties change
        //and even when the game isn't running
        if (exhaustButton != null)
            UpdateButton();
    }

    private void OnExhaustClick()
    {
        if (IsExhaustEnabled())
            gameGrid.ClearTiles();
    }

    private void UpdateButton()
    {
        //exhaustButton.interactable = IsExhaustEnabled();
        //exhaustButton.gameObject.SetActive(IsExhaustEnabled());
        bool isEnabled = IsExhaustEnabled();
        buttonCanvasGroup.interactable = isEnabled;
        buttonCanvasGroup.DOFade(isEnabled ? 1f : 0f, 0.2f);
    }
}
