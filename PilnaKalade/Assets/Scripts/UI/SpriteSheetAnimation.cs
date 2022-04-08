using UnityEngine;
using UnityEngine.UI;

public class SpriteSheetAnimation : MonoBehaviour
{
    public Sprite[] sprites;
    
    private int spriteIndex = 1;
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        InvokeRepeating(nameof(PlayAnimation), 0f, 0.2f);
    }

    private void PlayAnimation() {
        image.sprite = sprites[spriteIndex++];
        if (spriteIndex >= sprites.Length)
            spriteIndex = 0;
    }
}
