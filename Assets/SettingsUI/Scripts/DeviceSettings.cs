using System.IO;
using UnityEngine;

public class DeviceSettings
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

    public static DeviceSettings LoadSettings(string filepath)
    {
        try
        {
            string json = File.ReadAllText(filepath);
            var settingsObject = JsonUtility.FromJson<DeviceSettings>(json);
            return settingsObject;
        }
        catch
        {
            return null;
        }
    }
    public void SaveSettings(string filepath)
    {
        try
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(filepath, json);
        }
        catch
        {
            Debug.Log("Nie udalo sie zapisac obiektu DeviceSettings do pliku!");
        }
    }
}
