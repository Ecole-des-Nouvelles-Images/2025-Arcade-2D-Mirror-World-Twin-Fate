using UnityEngine;

public class EnemyHitFeedback : MonoBehaviour
{
    [SerializeField] private float scaleMultiplier = 1.2f;      // Taille max lors du hit
    [SerializeField] private float animationDuration = 0.1f;    // Vitesse de l’effet
    [SerializeField] private bool isAnimating = false;
    
    private Vector3 originalScale;
    
    void Start()
    {
        originalScale = transform.localScale;
    }

    public void PlayHitAnimation()
    {
        if (!isAnimating)
            StartCoroutine(HitAnimation());
    }

    private System.Collections.IEnumerator HitAnimation()
    {
        isAnimating = true;

        Vector3 targetScale = originalScale * scaleMultiplier;
        float timer = 0f;

        // Étape 1 : Grossir
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / animationDuration);
            yield return null;
        }

        timer = 0f;

        // Étape 2 : Rétrécir
        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / animationDuration);
            yield return null;
        }

        transform.localScale = originalScale;
        isAnimating = false;
    }
}
