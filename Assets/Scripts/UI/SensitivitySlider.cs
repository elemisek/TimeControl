using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class SensitivitySlider : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public static float mouseSensitivity = 100;

    private void Start()
    {
        slider.value = 100;

        // Apply slider value to text field 
        slider.onValueChanged.AddListener((v) => { sliderText.text = v.ToString(); });
    }
    private void Update()
    {
        mouseSensitivity = slider.value;
    }
}
