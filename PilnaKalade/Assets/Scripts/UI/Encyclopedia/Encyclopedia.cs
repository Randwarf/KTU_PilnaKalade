using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Encyclopedia : MonoBehaviour
{
    public GameObject CardPrefab;
    public Transform CardGrid;

    public CanvasGroup CanvasGroup;

    void Start()
    {
        DisplayCards();
    }

    public void Open() {
        gameObject.SetActive(true);
        CanvasGroup.DOFade(1f, 0.2f);
    }

    public void Close() {
        CanvasGroup.DOFade(0f, 0.2f).OnComplete(() => gameObject.SetActive(false));
    }

    private void DisplayCards() {
        List<CardData> cards = CardDatabase.GetCards();
        foreach (CardData cardData in cards) {
            GameObject cardGO = Instantiate(CardPrefab, CardGrid);
            Card card = cardGO.GetComponent<Card>();
            card.SetData(cardData);
            card.UpdateVisuals();
        }
    }
}