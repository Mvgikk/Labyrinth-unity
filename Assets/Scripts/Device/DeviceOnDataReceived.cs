using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeviceOnDataReceived
{
    public static void OnHeartRateReceived(int hr)
    {
        Debug.Log("Heart rate: " + hr);
    }
    public static void OnWearStateReceived()
    {
        Debug.Log("Wear state: " + Aidlab.AidlabSDK.aidlabDelegate.wearState.value);
    }
    public static void OnTemperatureReceived()
    {
        Debug.Log("Temperature: " + Aidlab.AidlabSDK.aidlabDelegate.temperature.value);
    }
    public static void OnRespirationReceived()
    {
        Debug.Log("Respiration: " + Aidlab.AidlabSDK.aidlabDelegate.respirationSignal.Count);
    }
    public static void OnRespirationRateReceived()
    {
        Debug.Log("Respiration rate: " + Aidlab.AidlabSDK.aidlabDelegate.respirationRate.value);
    }
    public static void OnRRReceived()
    {
        Debug.Log("RR: " + Aidlab.AidlabSDK.aidlabDelegate.rr.value);
    }
    public static void OnPressureReceived()
    {
        Debug.Log("Pressure: " + Aidlab.AidlabSDK.aidlabDelegate.pressure.value);
    }
    public static void OnOrientationReceived()
    {
        Debug.Log($"Orientation:\n\t " +
            $"x: {Aidlab.AidlabSDK.aidlabDelegate.orientation.x}\t" +
            $"y: {Aidlab.AidlabSDK.aidlabDelegate.orientation.y}\t" +
            $"z: {Aidlab.AidlabSDK.aidlabDelegate.orientation.z}\t");
    }
}
