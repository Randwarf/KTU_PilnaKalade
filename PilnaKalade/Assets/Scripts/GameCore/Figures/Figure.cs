using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class Figure : MonoBehaviour
{
    private EventSystem EventSystem;
    private GraphicRaycaster Raycaster;
    private GameGrid grid;

    private int[,] figureMap;
    private List<Image> figureTiles;

    private GameCard card;

    void Start() {
        EventSystem = FindObjectOfType<EventSystem>();
        Raycaster = FindObjectOfType<GraphicRaycaster>();
        grid = FindObjectOfType<GameGrid>();
        GetFigureTiles();
    }

    public void SetCard(GameCard card) {
        this.card = card;
    }

    public void FadeIn() {
        GetFigureTiles();
        figureTiles.ForEach(tile => tile.DOFade(1f, 0.2f));
    }

    public void Show() {
        GetFigureTiles();
        figureTiles.ForEach(tile => tile.DOFade(1f, 0f));
    }

    public void ShowSelection() {
        if (grid == null)
            return;

        grid.UnmarkTiles();
        grid.MarkTiles(GetSelectedGridTiles());
    }

    public bool PlaceFigure() {
        var selectedGridTiles = GetSelectedGridTiles();

        if (selectedGridTiles.Count != figureTiles.Count 
            || selectedGridTiles.Any(grid.IsTileOccupied)) {
            grid.UnmarkTiles();
            return false;
        }

        grid.PlaceTiles(GetSelectedGridTiles());
        return true;
    }

    public void SetFigure(int[,] figureMap) {
        this.figureMap = figureMap;
    }

    private void GetFigureTiles() {
        var allFigureTiles = GetComponentsInChildren<Image>();
        figureTiles = new List<Image>();

        for (int i = 0; i < figureMap.GetLength(0); i++) {
            for (int j = 0; j < figureMap.GetLength(1); j++) {
                if (figureMap[i, j] == 1) {
                    int index = figureMap.GetLength(1) * i + j;
                    figureTiles.Add(allFigureTiles[index]);
                }
            }
        }
    }

    private List<int> GetSelectedGridTiles() {
        List<int> selectedTiles = new List<int>();

        foreach (Image figureTile in figureTiles) {
            PointerEventData pointerEventData = new PointerEventData(EventSystem);
            pointerEventData.position = figureTile.transform.position;
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            Raycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results) {
                if (result.gameObject.CompareTag("Tile")) {
                    int siblingIndex = result.gameObject.transform.GetSiblingIndex();
                    selectedTiles.Add(siblingIndex);
                }
            }
        }

        return selectedTiles;
    }

    private void OnDestroy() {
        figureTiles.ForEach(tile => {
            DOTween.Complete(tile);
        });
        if (card != null) {
            card.KillTweens();
            card.Show();
        }
    }
}
