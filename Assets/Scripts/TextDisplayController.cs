using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplayController : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public float displayDuration = 3f;
    private Coroutine displayCoroutine;

    public void UpdateText(string text)
    {
        displayText.text = text;
        if (displayCoroutine != null)
        {
            StopCoroutine(displayCoroutine); // Stop the coroutine if it's already running
        }
        displayCoroutine = StartCoroutine(ClearTextAfterDelay(displayDuration));
    }

        private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        displayText.text = ""; // Clear the text
    }
}
