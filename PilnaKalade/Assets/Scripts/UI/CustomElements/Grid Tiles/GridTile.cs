using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTile : MonoBehaviour
{
    [SerializeField] private Image OuterOutline;
    [SerializeField] private Image InnerOutline;
    [SerializeField] private Image Fill;
    [SerializeField] private Image Shadow;

    [SerializeField] private float ShadowHeight = 8;

    public bool PlacedOn { get; private set; }

    private Color OuterOutlineColor;
    private Color InnerOutlineColor;
    private Color FillColor;

    private void Start() {
        OuterOutlineColor = OuterOutline.color;
        InnerOutlineColor = InnerOutline.color;
        FillColor = Fill.color;
    }

    public void Place() {
        if (PlacedOn == true) {
            return;
        }

        PlacedOn = true;

        float outerOutlineY = OuterOutline.transform.localPosition.y;
        OuterOutline.transform.DOLocalMoveY(outerOutlineY - ShadowHeight, 0.2f);

        OuterOutline.DOColor(new Color32(205, 164, 0, 255), 0.2f);
        InnerOutline.DOColor(new Color32(255, 217, 72, 255), 0.2f);
        Fill.DOColor(new Color32(255, 204, 0, 255), 0.2f);
    }

    public void Unplace() {
        if (PlacedOn == false) {
            return;
        }

        PlacedOn = false;

        float outerOutlineY = OuterOutline.transform.localPosition.y;
        OuterOutline.transform.DOLocalMoveY(outerOutlineY + ShadowHeight, 0.2f);

        OuterOutline.DOColor(OuterOutlineColor, 0.2f);
        InnerOutline.DOColor(InnerOutlineColor, 0.2f);
        Fill.DOColor(FillColor, 0.2f);
    }

    public void Highlight() {
        // Could be different colors depending on card color (rarity?) like in the design
        // now just hardcoding it...
        InnerOutline.color = new Color32(255, 217, 72, 255);
    }

    public void SetTileColor(Color32 outerOutline, Color32 innerOutline, Color32 fill) {
        OuterOutline.color = outerOutline;
        InnerOutline.color = innerOutline;
        Fill.color = fill;
    }

    public void SetShadowColor(Color32 color) {
        Shadow.color = color;
    }

    public void ResetColor() {
        OuterOutline.color = OuterOutlineColor;
        InnerOutline.color = InnerOutlineColor;
        Fill.color = FillColor;
    }

    private void OnDestroy() {
        DOTween.Kill(OuterOutline.transform);
        DOTween.Kill(OuterOutline);
        DOTween.Kill(InnerOutline);
        DOTween.Kill(Fill);
    }
}
