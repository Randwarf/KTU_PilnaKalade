using Assets.Scripts.UI.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameGrid : MonoBehaviour
{
    private GridTile[] tiles;
    private List<int> markedTiles;

    private List<int> placedTiles;
    public UnityEvent onTilePlacementChanges = new UnityEvent();

    void Start() {
        markedTiles = new List<int>();
        placedTiles = new List<int>();
        tiles = GetComponentsInChildren<GridTile>();
    }

    public bool IsTileOccupied(int index) {
        return placedTiles.Contains(index);
    }

    public float GetPlacedTilesProportion()
    {
        if (tiles is null || placedTiles is null)
            return 0.0f;

        return (float)placedTiles.Count / tiles.Length;
    }

    public void MarkTiles(List<int> indexes) {
        foreach (int index in indexes) {
            if (!placedTiles.Contains(index)) {
                tiles[index].Highlight();
                markedTiles.Add(index);
            }
        }
    }

    public void UnmarkTiles() {
        foreach (int markedTile in markedTiles) {
            tiles[markedTile].ResetColor();
        }
    }

    public void PlaceTiles(List<int> indexes) {
        markedTiles.Clear();
        foreach (int index in indexes) {
            tiles[index].Place();
            placedTiles.Add(index);
        }

        onTilePlacementChanges.Invoke();
    }

    public void ClearTiles()
    {
        markedTiles.Clear();
        placedTiles.Clear();
        foreach (var tile in tiles)
            tile.Unplace();

        onTilePlacementChanges.Invoke();
    }
}
