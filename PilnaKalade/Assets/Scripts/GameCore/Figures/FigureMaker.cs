using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FigureMaker {
    public static Figure SpawnFigure(int[,] figureMap, Transform parent, Vector2 localPosition, float cellSize, float spacing) {
        GameObject figure = new GameObject("Figure");
        figure.transform.SetParent(parent);

        // Configure Grid Layout Group component
        var grid = figure.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(cellSize, cellSize);
        grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        grid.constraintCount = figureMap.GetLength(0);
        grid.spacing = new Vector2(spacing, spacing);

        // Adding Content Size Fitter
        var fitter = figure.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

        // Figure size configuration
        var rect = figure.GetComponent<RectTransform>();
        float totalSquareSize = grid.cellSize.x * grid.constraintCount;
        float totalSpacing = grid.spacing.x * (grid.constraintCount - 1);
        float totalSize = totalSquareSize + totalSpacing;
        rect.sizeDelta = new Vector2(totalSize, totalSize);

        // Spawning squares
        Sprite sprite = Resources.Load<Sprite>("Art/Figure Tile");
        for (int i = 0; i < figureMap.GetLength(0); i++) {
            for (int j = 0; j < figureMap.GetLength(1); j++) {
                GameObject square = new GameObject("Square");
                square.transform.SetParent(grid.transform);
                Image squareImg = square.AddComponent<Image>();
                squareImg.sprite = sprite; 
                if (figureMap[i, j] == 0) {
                    squareImg.color = new Color(0, 0, 0, 0);
                } else {
                    //squareImg.color = Color.cyan;
                    squareImg.DOFade(0, 0);
                }
            }
        }

        figure.transform.localScale = Vector3.one;
        figure.transform.localPosition = localPosition;
        figure.AddComponent<Figure>().SetFigure(figureMap);
        return figure.GetComponent<Figure>();
    }
}
