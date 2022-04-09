using System;

[Serializable]
public class CardData
{
    public int key;
    public string id;
    public int cost;
    public int rarity;
    public string title;
    public string description;
    public string figureMap;
    public CardStats stats;

    public int[,] GetFigureMap() {
        string figureMapString = figureMap;
        string[] rows = figureMapString.Split(' ');
        int[,] figureMapArray = new int[rows.Length, rows[0].Length];
        for (int i = 0; i < rows.Length; i++) {
            for (int j = 0; j < rows[i].Length; j++) {
                int value = (int)char.GetNumericValue(rows[i][j]);
                figureMapArray[i, j] = value;
            }
        }
        return figureMapArray;
    }
}
