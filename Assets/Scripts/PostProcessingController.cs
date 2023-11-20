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
    private AnalogGlitchVolume analogGlitchVolume;
    private DigitalGlitchVolume digitalGlitchVolume;
    private AudioSource audioSource;
    private bool playsSoundEffect = false;

    public AudioClip glitchSound;

    public Player player;
    public List<Monster> monsters = new List<Monster>();

    public int glitchRange = 10;

    void Start()
    {
        volume = GetComponent<Volume>();
        volume.profile.TryGet<AnalogGlitchVolume>(out analogGlitchVolume);
        volume.profile.TryGet<DigitalGlitchVolume>(out digitalGlitchVolume);
        audioSource = GetComponent<AudioSource>();

        Monster[] monsterArray = GameObject.FindObjectsOfType<Monster>();
        monsters = new List<Monster>(monsterArray);
    }
    
    // Update is called once per frame
    void Update()
    {
        float fearLevel = player.fearLevel;
        bool monsterNearby = false;

        foreach (Monster monster in monsters)
        {
            if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y),
                                new Vector2(monster.transform.position.x, monster.transform.position.y)) < glitchRange)
            {
                monsterNearby = true;
            }
        }

        if(fearLevel > 85 || monsterNearby)
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
