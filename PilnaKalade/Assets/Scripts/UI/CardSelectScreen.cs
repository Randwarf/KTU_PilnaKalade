using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class CardSelectScreen : MonoBehaviour
{
    public List<CardData> DrawnCards;

    public Button SkipButton;

    public UnityEvent OnSelected = new UnityEvent();

    public GameObject CardPrefab;

    public GameObject CardGrid;

    private const int selectCardCount = 3;

    public void Start()
    {
        DrawnCards = new List<CardData>();
        for(int i = 0; i < selectCardCount; i++) {
            DrawnCards.Add(CardDatabase.GetRandomCard());

            var cardGameObject = GameObject.Instantiate(CardPrefab, CardGrid.transform);

            EncyclopediaCard card = cardGameObject.GetComponent<EncyclopediaCard>();
            card.SetData(DrawnCards[i]);
            card.UpdateVisuals();

            Button button = cardGameObject.GetComponent<Button>();
            int index = i;
            button.onClick.AddListener(() => OnCardClick(index));
        }

        SkipButton.onClick.AddListener(OnSkipClick);
    }

    public void OnSkipClick() 
    {
        OnSelected.Invoke();
        Destroy(gameObject);
    }

    public void OnCardClick(int index) 
    {
        var selectedCard = DrawnCards[index];
        PlayerDeck.AddCard(selectedCard);
        //CardDatabase.AddNewCard(selectedCard);

        OnSelected.Invoke();
        Destroy(gameObject);
    }
}