using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider Slider;

    public void SetValue(int value)
    {
        Slider.value = value;
    }

    public void SetMaxValue(int maxValue)
    {
        Slider.maxValue = maxValue;
        
        SetValue(maxValue);
    }
}
