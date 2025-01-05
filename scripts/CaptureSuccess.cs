using UnityEngine;
using TMPro; // Add TMPro namespace
using System.Collections;

public class CaptureSuccess : MonoBehaviour
{
    public TextMeshProUGUI feedbackText; // Change to TextMeshProUGUI
    private float displayDuration = 2f;

    public void ShowFeedback(bool success)
    {
        feedbackText.text = success ? "Success!" : "Try again!";
        feedbackText.color = success ? Color.green : Color.red;
        StartCoroutine(FadeOutText());
    }

    private IEnumerator FadeOutText()
    {
        yield return new WaitForSeconds(displayDuration);
        float fadeTime = 1f;
        float elapsedTime = 0f;
        Color startColor = feedbackText.color;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            feedbackText.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            yield return null;
        }

        feedbackText.text = "";
    }
}