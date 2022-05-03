using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScore : MonoBehaviour
{
    private TextMeshProUGUI tmproText;

    private void Start()
    {
        tmproText = GetComponent<TextMeshProUGUI>();

        var slider = GetComponentInParent<Slider>();
        slider.onValueChanged.AddListener(HandleValueChanged);

        tmproText.text = slider.value.ToString();
    }

    private void HandleValueChanged(float value)
    {
        if(value <= 0)
        {
            tmproText.text = "";
            return;
        }

        tmproText.text = value.ToString();
    }
}