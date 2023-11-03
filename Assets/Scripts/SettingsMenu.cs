using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    
    public AudioMixer audioMixer;
    Resolution[] resolutions;

    public TMPro.TMP_Dropdown resolutionDropdown;

    public TMPro.TMP_Dropdown qualityDropdown;

    public Toggle simulationToggle;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SimulationSettings.isSimulated = simulationToggle.isOn;

        qualityDropdown.value = 2;
        qualityDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume",volume);
    }

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetSimulation (bool isSimulated)
    {
        SimulationSettings.isSimulated = isSimulated;
    }

    public void SetSimulationLevel (int simulationIndex)
    {
        SimulationSettings.simulationLevel = (SimulationLevel)simulationIndex;
    }

    public void ShowSavedSettings()
    {
        Debug.Log("Simulate HR: " + SimulationSettings.isSimulated);
        Debug.Log("Simulation Level: " + SimulationSettings.simulationLevel);
        Debug.Log("Quality: " + QualitySettings.GetQualityLevel());
    }
}
