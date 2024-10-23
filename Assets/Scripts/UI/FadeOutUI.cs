using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        // Get the CanvasGroup component attached to this GameObject
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void StartFadeOut(float duration)
    {
        Debug.Log("StartFadeOut called with duration: " + duration);
        StartCoroutine(FadeOut(duration));
    }

    private IEnumerator FadeOut(float duration)
    {
        Debug.Log("FadeOut coroutine started.");
        float startAlpha = canvasGroup.alpha;
        float endAlpha = 0f;
        float elapsedTime = 0f;

        // While the elapsed time is less than the duration
        while (elapsedTime < duration)
        {
            // Calculate the new alpha value
            elapsedTime += Time.unscaledDeltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            canvasGroup.alpha = newAlpha;
            Debug.Log("elapsedTime: " + elapsedTime + " -- alpha " + canvasGroup.alpha);

            // Optional: You can yield return null to wait for the next frame
            yield return null;
        }

        // Ensure the final alpha is set to 0
        canvasGroup.alpha = endAlpha;
        Time.timeScale = 1;
        // Optionally, disable the GameObject after fading out
        gameObject.SetActive(false);
    }
}
