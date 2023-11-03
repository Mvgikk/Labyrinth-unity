using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;



public class PostProcessingController : MonoBehaviour
{

    private Volume volume;
    //private LensDistortion lensDistortion;
    private AnalogGlitchVolume analogGlitchVolume;
    private DigitalGlitchVolume digitalGlitchVolume;
    private AudioSource audioSource;
    private bool playsSoundEffect = false;
   // private Animator fearGlitchAnimator;

   public AudioClip glitchSound;

    public Player player;
    void Start()
    {
        volume = GetComponent<Volume>();

        volume.profile.TryGet<AnalogGlitchVolume>(out analogGlitchVolume);
        volume.profile.TryGet<DigitalGlitchVolume>(out digitalGlitchVolume);
       // fearGlitchAnimator = GetComponent<Animator>();
       audioSource = GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    void Update()
    {

       float fearLevel = player.fearLevel;
        //fearLevel = 100;
        //Debug.Log("FEARLEVEL " + fearLevel);
        //digitalGlitchVolume.intensity.value 


        //fearGlitchAnimator.SetFloat("FearLevel", fearLevel);

        //digitalGlitchVolume.intensity.value = Mathf.Lerp(0f, 1f, fearLevel / 100f); 
        if(fearLevel > 85)
        {
            volume.enabled = true;
            if (!playsSoundEffect) 
            {
                StartCoroutine(PlaySoundAndWait(glitchSound));
            }

        }
        else
        {
            volume.enabled = false;
            audioSource.Stop();
        }



    }
    IEnumerator PlaySoundAndWait(AudioClip sound)
    {

        playsSoundEffect = true;

        audioSource.PlayOneShot(sound);
        // Wait until the sound finishes playing
        yield return new WaitForSeconds(sound.length);
        playsSoundEffect = false;

    }   



}
