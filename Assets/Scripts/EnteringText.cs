using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnteringText : MonoBehaviour
{
    public TextMeshProUGUI enteringText;
    public float displayDuration = 5f;
    private Coroutine displayCoroutine;


    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        enteringText.text = ""; // Clear the text
    }


    private void Start() 
    {
 
        displayCoroutine = StartCoroutine(ClearTextAfterDelay(displayDuration));
    }
}
