using Assets.Scripts.UI.Stats;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider Slider;

    public Slider BackgroundSlider;

    public BarType Type;

    public int Value { 
        get => Convert.ToInt32(Slider.value); 
        
        set => Slider.value = value;
    }

    public float MaxValue
    {
        set
        {
            Slider.maxValue = value;
            Slider.value = value;

            BackgroundSlider.maxValue = value;
            BackgroundSlider.value = value;
        }

        get => Slider.maxValue;
    }

    public void CompleteSynchronization()
    {
        BackgroundSlider.value = Slider.value;
    }

    public void CancelSynchronization()
    {
        Slider.value = BackgroundSlider.value;
    }
}
