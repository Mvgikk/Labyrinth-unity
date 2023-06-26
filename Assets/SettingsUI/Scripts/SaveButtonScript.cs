using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveButtonScript : MonoBehaviour
{
    public void BackToMenu()
    {
        // SceneManager.LoadScene("MainMenu");
        this.gameObject.SetActive(false);
    }

    public void SaveAndExit()
    {
        var deviceSettings = GetComponentInChildren<PanelEvents>().deviceSettings;
        deviceSettings.SaveSettings("device_settings.json");
        BackToMenu();
    }
}
