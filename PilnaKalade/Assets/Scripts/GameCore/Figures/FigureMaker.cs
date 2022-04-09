using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FigureMaker {
    public static Figure SpawnFigure(int[,] figureMap, Canvas canvas, Vector2 position) {
        GameObject figure = new GameObject("Figure");
        figure.transform.SetParent(canvas.transform);

        // Configure Grid Layout Group component
        var grid = figure.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(100, 100);
        grid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        grid.constraintCount = figureMap.GetLength(0);
        grid.spacing = new Vector2(5, 5);

        // Figure size configuration
        var rect = figure.GetComponent<RectTransform>();
        float totalSquareSize = grid.cellSize.x * grid.constraintCount;
        float totalSpacing = grid.spacing.x * (grid.constraintCount - 1);
        float totalSize = totalSquareSize + totalSpacing;
        rect.sizeDelta = new Vector2(totalSize, totalSize);

        // Spawning Raw Images
        for (int i = 0; i < figureMap.GetLength(0); i++) {
            for (int j = 0; j < figureMap.GetLength(1); j++) {
                GameObject square = new GameObject("Square");
                square.transform.SetParent(grid.transform);
                RawImage squareRI = square.AddComponent<RawImage>();
                if (figureMap[i, j] == 0) {
                    squareRI.color = new Color(0, 0, 0, 0);
                } else {
                    squareRI.color = Color.cyan;
                    squareRI.DOFade(0, 0);
                }
            }
        }

        figure.transform.localScale = Vector3.one;
        figure.transform.localPosition = position;
        figure.AddComponent<Figure>().SetFigure(figureMap);
        return figure.GetComponent<Figure>();
    }
}
