using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Figure : MonoBehaviour
{
    private Canvas canvas;
    private EventSystem EventSystem;
    private GraphicRaycaster Raycaster;

    private int[,] figureMap;
    private List<RawImage> figureTiles;

    void Start() {
        canvas = FindObjectOfType<Canvas>();
        EventSystem = FindObjectOfType<EventSystem>();
        Raycaster = FindObjectOfType<GraphicRaycaster>();
        GetFigureTiles();
    }

    void Update() {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform, Input.mousePosition,
            canvas.worldCamera,
            out Vector2 pos);

        // Add lerp?
        transform.localPosition = pos;

        //if (Input.GetKeyDown(KeyCode.Space)) {
            GameGrid grid = FindObjectOfType<GameGrid>();
            grid.UnmarkTiles();
            grid.MarkTiles(GetSelectedGridTiles(), Color.yellow);
        //}
    }

    public void SetFigure(int[,] figureMap) {
        this.figureMap = figureMap;
    }

    private void GetFigureTiles() {
        var allFigureTiles = GetComponentsInChildren<RawImage>();
        figureTiles = new List<RawImage>();

        for (int i = 0; i < figureMap.GetLength(0); i++) {
            for (int j = 0; j < figureMap.GetLength(1); j++) {
                if (figureMap[i, j] == 1) {
                    int index = figureMap.GetLength(0) * i + j;
                    figureTiles.Add(allFigureTiles[index]);
                }
            }
        }
    }

    private List<int> GetSelectedGridTiles() {
        List<int> selectedTiles = new List<int>();

        foreach (RawImage figureTile in figureTiles) {
            PointerEventData pointerEventData = new PointerEventData(EventSystem);
            pointerEventData.position = figureTile.transform.position;
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            Raycaster.Raycast(pointerEventData, results);

            // Jei resultat? tiek pat kiek tiles figuroje, tada placinimas viable
            foreach (RaycastResult result in results) {
                if (result.gameObject.CompareTag("Tile")) {
                    int siblingIndex = result.gameObject.transform.GetSiblingIndex();
                    selectedTiles.Add(siblingIndex);
                }
            }
        }

        return selectedTiles;
    }
}
