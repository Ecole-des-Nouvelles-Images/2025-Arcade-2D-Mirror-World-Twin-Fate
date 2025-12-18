using UnityEngine;
using UnityEngine.UI;
public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Image fill;
    [SerializeField] private Image background;

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 0.4f;

    private bool isVisible = false;

    private void Awake()
    {
        SetAlpha(0f);
    }

    public void Show()
    {
        if (isVisible) return;
        isVisible = true;

        fill.enabled = true;
        background.enabled = true;

        FadeTo(1f);
    }

    public void Hide()
    {
        if (!isVisible) return;
        isVisible = false;

        FadeTo(0f);
    }

    public void SetHealth(float current, float max)
    {
        fill.fillAmount = current / max;
    }

    // --------------------
    // Fade helpers
    // --------------------

    private void FadeTo(float targetAlpha)
    {
        fill.canvasRenderer.SetAlpha(fill.canvasRenderer.GetAlpha());
        background.canvasRenderer.SetAlpha(background.canvasRenderer.GetAlpha());

        fill.CrossFadeAlpha(targetAlpha, fadeDuration, false);
        background.CrossFadeAlpha(targetAlpha, fadeDuration, false);
    }

    private void SetAlpha(float alpha)
    {
        fill.canvasRenderer.SetAlpha(alpha);
        background.canvasRenderer.SetAlpha(alpha);
    }
}
