using Assets.Scripts.UI.Stats;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    //public Slider Slider;

    //public Slider BackgroundSlider;

    public StatusBar StatusBar;

    public BarType Type;

    public int Value
    {
        //get => Convert.ToInt32(Slider.value); 

        //set => Slider.value = value;

        get => StatusBar.Value;

        set => StatusBar.SetValue(value);
    }

    public float MaxValue
    {
        //set
        //{
        //    Slider.maxValue = value;
        //    Slider.value = value;

        //    BackgroundSlider.maxValue = value;
        //    BackgroundSlider.value = value;
        //}

        //get => Slider.maxValue;

        set
        {
            Debug.Log(value);
            StatusBar.SetMaxValue(value);
            StatusBar.SetValue(value);
        }

        get => StatusBar.MaxValue;
    }

    public void CompleteSynchronization()
    {
        //BackgroundSlider.value = Slider.value;
        StatusBar.SetMaxValue(StatusBar.Value);
    }

    public void CancelSynchronization()
    {
        //Slider.value = BackgroundSlider.value;
        StatusBar.SetValue(StatusBar.MaxValue);
    }
}
