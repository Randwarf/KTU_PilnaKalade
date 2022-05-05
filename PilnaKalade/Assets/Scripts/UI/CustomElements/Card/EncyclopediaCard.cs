using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EncyclopediaCard : MonoBehaviour
{
    [SerializeField] private Transform PrimaryColorPanel;
    [SerializeField] private Text FigureNameText;
    [SerializeField] private Text FigureDescriptionText;
    [SerializeField] private Text CostText;

    public CardData CardData { get; private set; }

    public void SetData(CardData cardData) {
        CardData = cardData;
    }

    public void UpdateVisuals() {
        FigureNameText.text = CardData.title;
        FigureDescriptionText.text = CardData.description;
        CostText.text = "COST: " + CardData.cost;
        int[,] figure = CardData.GetFigureMap();
        FigureMaker.SpawnFigure(figure, PrimaryColorPanel, new Vector2(0, 100), 75, 2).Show();
    }
}
