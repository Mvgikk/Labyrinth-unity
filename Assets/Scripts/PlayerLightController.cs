using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightController : MonoBehaviour
{
    public float decreaseRate = 0.05f; // Rate at which the light intensity decreases per second
    public Light2D playerLight;
    public float minValue = 0.65f;

    private void Start()
    {

        playerLight = GetComponent<Light2D>();
    }

    private void Update()
    {
        // Decrease the light intensity over time
        float newIntensity = playerLight.intensity - decreaseRate * Time.deltaTime;
        newIntensity = Mathf.Clamp(newIntensity, minValue, playerLight.intensity); // Ensure the intensity doesn't go below 0.1
        playerLight.intensity = newIntensity;
    }

    public void increasePlayerLightOuterRadius()
    {
        playerLight.pointLightOuterRadius +=1.0f;
    }
}
