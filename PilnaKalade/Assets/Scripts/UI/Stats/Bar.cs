using Assets.Scripts.UI.Stats;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Slider Slider;
    public BarType Type;

    public float Value { 
        get 
        { 
            return Slider.value; 
        }

        set
        {
            Slider.value = value;
        }
    }

    public float MaxValue
    {
        set
        {
            Slider.maxValue = value;
            Slider.value = value;
        }
    }
}
