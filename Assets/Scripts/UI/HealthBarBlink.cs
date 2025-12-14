using UnityEngine;
using UnityEngine.UI;
public class HealthBarBlink : MonoBehaviour
{
    public Image healthBar;

    [Range(0f, 1f)]
    public float lowHealthThreshold = 0.2f;

    public float blinkSpeed = 4f;

    private Color normalColor;
    public Color warningColor = Color.red;

    void Start()
    {
        normalColor = healthBar.color;
    }

    void Update()
    {
        if (healthBar.fillAmount <= lowHealthThreshold)
        {
            float t = Mathf.PingPong(Time.time * blinkSpeed, 1f);
            healthBar.color = Color.Lerp(normalColor, warningColor, t);
        }
        else
        {
            healthBar.color = normalColor;
        }
    }
}
