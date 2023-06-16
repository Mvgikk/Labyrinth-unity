using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   

    public AudioClip clickEffect;
    public AudioSource audioSource;

    private static SoundManager soundManagerInstance;

    private void Awake() 
    {
        //music plays continuously without it starting over while switching scenes
        if (soundManagerInstance==null)
        {
            soundManagerInstance = this;
            DontDestroyOnLoad(soundManagerInstance);
        }
        else{
            Destroy(soundManagerInstance);
        }
    }



     public void PlayClickEffect()
    {
        
        audioSource.PlayOneShot(clickEffect);
    }

}
