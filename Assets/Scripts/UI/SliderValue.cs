using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderValue : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI valueText;

    void Start()
    {
        UpdateValue(slider.value);

        // On ajoute un listener pour détecter les changements du slider
        slider.onValueChanged.AddListener(delegate { UpdateValue(slider.value); });
    }

    void UpdateValue(float value)
    {
        valueText.text = Mathf.RoundToInt(value*100).ToString();
    }
}
