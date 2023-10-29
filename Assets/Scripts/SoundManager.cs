using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{   

    public AudioClip clickEffect;
    public AudioClip coinCollectEffect;
    public AudioClip chestOpenEffect;
    public AudioClip monsterScreechSound;
    public AudioClip boxEnterSound;
    public AudioClip deathSound;
    public AudioSource audioSource;

    public AudioClip typeWriterEffect;

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

    public void PlayCoinCollectEffect()
    {
        
        audioSource.PlayOneShot(coinCollectEffect);
    }
        public void PlayChestOpenEffect()
    {
        
        audioSource.PlayOneShot(chestOpenEffect);
    }

        public void PlayMonsterScreechEffect()
    {
        
        audioSource.PlayOneShot(monsterScreechSound);
    }

    public void PlayBoxEnterEffect()
    {

        audioSource.PlayOneShot(boxEnterSound);
    }
        public void PlayDeathSound()
    {

        audioSource.PlayOneShot(deathSound);
    }

    public void PlayTypeWriterEffect()
    {

        audioSource.PlayOneShot(typeWriterEffect);
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }
}
