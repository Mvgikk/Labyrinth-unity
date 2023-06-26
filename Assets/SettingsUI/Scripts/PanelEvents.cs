using UnityEngine;
using UnityEngine.UI;

public class PanelEvents : MonoBehaviour
{
    [SerializeField] GameObject WearStateToggle;
    [SerializeField] GameObject HeartRateToggle;
    [SerializeField] GameObject TemperatureToggle;
    [SerializeField] GameObject RespirationToggle;
    [SerializeField] GameObject RespirationRateToggle;
    [SerializeField] GameObject RRToggle;
    [SerializeField] GameObject PressureToggle;
    [SerializeField] GameObject OrientationToggle;
    [SerializeField] GameObject ECGPlotToggle;
    [SerializeField] GameObject ECGPeaksDotsToggle;

    public DeviceSettings deviceSettings;

    private void Start()
    {
        Toggle toggle;
        InitializeSettings();

        #region Toggles
        toggle = WearStateToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveWearState);
            toggle.isOn = deviceSettings.WearState;
        }

        toggle = HeartRateToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveHR);
            toggle.isOn = deviceSettings.HeartRate;
        }
        toggle = TemperatureToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveTemperature);
            toggle.isOn = deviceSettings.Temperature;
        }

        toggle = RespirationToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveRespiration);
            toggle.isOn = deviceSettings.Respiration;
        }
        toggle = RespirationRateToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveRespirationRate);
            toggle.isOn = deviceSettings.RespirationRate;
        }

        toggle = RRToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveRR);
            toggle.isOn = deviceSettings.RR;
        }

        toggle = PressureToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceivePressure);
            toggle.isOn = deviceSettings.Pressure;
        }

        toggle = OrientationToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveOrientation);
            toggle.isOn = deviceSettings.Orientation;
        }

        toggle = ECGPlotToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(EnablePlotting);
            toggle.isOn = deviceSettings.PlotECG;
        }

        toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(EnablePlottingDetectedPeaks);
            toggle.isOn = deviceSettings.PlotDetectedPeaks;
        }

        #endregion Toggles

    }

    public void InitializeSettings()
    {
        deviceSettings = DeviceSettings.LoadSettings("device_settings.json");
        if (deviceSettings == null)
            deviceSettings = new DeviceSettings();
    }

    public void ReceiveWearState(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.wearState.Subscribe(DeviceOnDataReceived.OnWearStateReceived);
            deviceSettings.WearState = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.wearState.Unsubscribe(DeviceOnDataReceived.OnWearStateReceived);
            deviceSettings.WearState = false;
        }
    }
    public void ReceiveHR(bool doReceive)
    {
        if (doReceive)
        {
            GameObject ECGReceiver = new GameObject("ECGReceiver");
            GameObject.DontDestroyOnLoad(ECGReceiver);

            ECGReceiver.AddComponent<ECGReceiver>();
            deviceSettings.HeartRate = true;
        }
        else
        {
            GameObject ECGReceiver = GameObject.Find("ECGReceiver");
            if (ECGReceiver)
                GameObject.Destroy(ECGReceiver);
            deviceSettings.HeartRate = false;
        }
    }
    public void ReceiveTemperature(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.temperature.Subscribe(DeviceOnDataReceived.OnTemperatureReceived);
            deviceSettings.Temperature = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.temperature.Unsubscribe(DeviceOnDataReceived.OnTemperatureReceived);
            deviceSettings.Temperature = false;
        }
    }
    public void ReceiveRespiration(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.respiration.Subscribe(DeviceOnDataReceived.OnRespirationReceived);
            deviceSettings.Respiration = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.respiration.Unsubscribe(DeviceOnDataReceived.OnRespirationReceived);
            deviceSettings.Respiration = false;
        }
    }
    public void ReceiveRespirationRate(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.respirationRate.Subscribe(DeviceOnDataReceived.OnRespirationRateReceived);
            deviceSettings.RespirationRate = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.respirationRate.Unsubscribe(DeviceOnDataReceived.OnRespirationRateReceived);
            deviceSettings.RespirationRate = false;
        }
    }
    public void ReceiveRR(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.rr.Subscribe(DeviceOnDataReceived.OnRRReceived);
            deviceSettings.RR = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.rr.Unsubscribe(DeviceOnDataReceived.OnRRReceived);
            deviceSettings.RR = false;
        }
    }
    public void ReceivePressure(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.pressure.Subscribe(DeviceOnDataReceived.OnPressureReceived);
            deviceSettings.Pressure = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.pressure.Unsubscribe(DeviceOnDataReceived.OnPressureReceived);
            deviceSettings.Pressure = false;
        }
    }
    public void ReceiveOrientation(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.orientation.Subscribe(DeviceOnDataReceived.OnOrientationReceived);
            deviceSettings.Orientation = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.orientation.Unsubscribe(DeviceOnDataReceived.OnOrientationReceived);
            deviceSettings.Orientation = false;
        }
    }
    public void EnablePlotting(bool doReceive)
    {
        if (doReceive)
        {
            GameObject FloatPlotter = new GameObject("FloatPlotter");
            GameObject.DontDestroyOnLoad(FloatPlotter);

            // FloatPlotter.transform.parent = GameObject.Find("Canvas").transform;
            FloatPlotter.transform.position = new Vector3(590, 225, 0);

            var FloatGraphComponent = FloatPlotter.AddComponent<FloatGraph>();
            GameObject ECGReceiver = GameObject.Find("ECGReceiver");
            if (ECGReceiver != null)
                ECGReceiver.GetComponent<ECGReceiver>().floatGraph = FloatGraphComponent;
            Toggle toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
            toggle.interactable = true;
            deviceSettings.PlotECG = true;
        }
        else
        {
            GameObject FloatPlotter = GameObject.Find("FloatPlotter");
            if (FloatPlotter)
                GameObject.Destroy(FloatPlotter);
            Toggle toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
            toggle.interactable = false;
            deviceSettings.PlotECG = false;
        }
    }
    public void EnablePlottingDetectedPeaks(bool doReceive)
    {
        GameObject FloatPlotter = GameObject.Find("FloatPlotter");
        var FloatGraphComponent = FloatPlotter.GetComponent<FloatGraph>();
        if (doReceive)
        {
            FloatGraphComponent.showDetectedPoitns = true;
            deviceSettings.PlotDetectedPeaks = true;
        }
        else
        {
            FloatGraphComponent.showDetectedPoitns = false;
            deviceSettings.PlotDetectedPeaks = false;
        }
    }
}
