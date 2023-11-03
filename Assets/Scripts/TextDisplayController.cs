using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplayController : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public float displayDuration = 6f;
    private Coroutine displayCoroutine;
    private Coroutine typeWriterCoroutine;
    public SoundManager soundManager;
    [SerializeField] float delayBeforeStart = 0f;
	[SerializeField] float timeBtwChars = 0.12f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay = false;


    public void UpdateText(string text)
    {
        //displayText.text = text;
        if (typeWriterCoroutine != null)
        {
            
            StopCoroutine(typeWriterCoroutine);
        }
        soundManager.PlayTypeWriterEffect();
        typeWriterCoroutine = StartCoroutine(TypeWriterTMP(text));

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

    	IEnumerator TypeWriterTMP(string text)
    {
        string writer = text;
        text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in writer)
		{
			if (displayText.text.Length > 0)
			{
				displayText.text = displayText.text.Substring(0, displayText.text.Length - leadingChar.Length);
			}
			displayText.text += c;
			displayText.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			displayText.text = displayText.text.Substring(0, displayText.text.Length - leadingChar.Length);
		}
        soundManager.StopPlaying();
	}
}
