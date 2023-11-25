using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class DeviceSettings : MonoBehaviour
{
    public class Settings
    {
        public bool WearState = false;
        public bool HeartRate = false;
        public bool Temperature = false;
        public bool Respiration = false;
        public bool RespirationRate = false;
        public bool RR = false;
        public bool Pressure = false;
        public bool Orientation = false;
        public bool PlotECG = false;
        public bool PlotDetectedPeaks = false;
        public bool ShowValues = false;
    }
    private static string settingsPath = $"{Directory.GetCurrentDirectory()}/device_settings.json";

    public Settings settings;

    private static DeviceSettings instance = null;
    public static DeviceSettings Instance {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DeviceSettings>();

            if (instance == null)
            {
                GameObject gObj = new GameObject();
                gObj.name = "DeviceSettings";
                instance = gObj.AddComponent<DeviceSettings>();
                instance.settings = LoadSettings();
                DontDestroyOnLoad(gObj);
            }
            return instance;
        }
    }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    private static Settings LoadSettings()
    {
        try
        {
            string json = File.ReadAllText(settingsPath);
            if (json.Length == 0)
                return new Settings();
            return JsonUtility.FromJson<Settings>(json);
        }
        catch
        {
            return new Settings();
        }
    }

    public void SaveSettings()
    {
        try
        {
            string json = JsonUtility.ToJson(settings);
            File.WriteAllText(settingsPath, json);
        }
        catch
        {
            Debug.Log("Nie udalo sie zapisac obiektu DeviceSettings do pliku!");
        }
    }
}
