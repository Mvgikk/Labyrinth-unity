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


    private void Start()
    {
        Toggle toggle;

        #region Toggles
        toggle = WearStateToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveWearState);
            toggle.isOn = DeviceSettings.Instance.settings.WearState;
        }

        toggle = HeartRateToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveHR);
            toggle.isOn = DeviceSettings.Instance.settings.HeartRate;
        }
        toggle = TemperatureToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveTemperature);
            toggle.isOn = DeviceSettings.Instance.settings.Temperature;
        }

        toggle = RespirationToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveRespiration);
            toggle.isOn = DeviceSettings.Instance.settings.Respiration;
        }
        toggle = RespirationRateToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveRespirationRate);
            toggle.isOn = DeviceSettings.Instance.settings.RespirationRate;
        }

        toggle = RRToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveRR);
            toggle.isOn = DeviceSettings.Instance.settings.RR;
        }

        toggle = PressureToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceivePressure);
            toggle.isOn = DeviceSettings.Instance.settings.Pressure;
        }

        toggle = OrientationToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(ReceiveOrientation);
            toggle.isOn = DeviceSettings.Instance.settings.Orientation;
        }

        toggle = ECGPlotToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(EnablePlotting);
            toggle.isOn = DeviceSettings.Instance.settings.PlotECG;
        }

        toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(EnablePlottingDetectedPeaks);
            toggle.isOn = DeviceSettings.Instance.settings.PlotDetectedPeaks;
        }

        #endregion Toggles

    }

    public void ReceiveWearState(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.wearState.Subscribe(DeviceOnDataReceived.OnWearStateReceived);
            DeviceSettings.Instance.settings.WearState = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.wearState.Unsubscribe(DeviceOnDataReceived.OnWearStateReceived);
            DeviceSettings.Instance.settings.WearState = false;
        }
    }
    public void ReceiveHR(bool doReceive)
    {
        GameObject ECGReceiver = GameObject.Find("ECGReceiver");
        if (doReceive)
        {
            if(ECGReceiver == null)
                ECGReceiver = new GameObject("ECGReceiver");
            GameObject.DontDestroyOnLoad(ECGReceiver);

            ECGReceiver.AddComponent<ECGReceiver>();
            DeviceSettings.Instance.settings.HeartRate = true;
        }
        else
        {
            if (ECGReceiver)
                GameObject.Destroy(ECGReceiver);
            DeviceSettings.Instance.settings.HeartRate = false;
        }
    }
    public void ReceiveTemperature(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.temperature.Subscribe(DeviceOnDataReceived.OnTemperatureReceived);
            DeviceSettings.Instance.settings.Temperature = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.temperature.Unsubscribe(DeviceOnDataReceived.OnTemperatureReceived);
            DeviceSettings.Instance.settings.Temperature = false;
        }
    }
    public void ReceiveRespiration(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.respiration.Subscribe(DeviceOnDataReceived.OnRespirationReceived);
            DeviceSettings.Instance.settings.Respiration = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.respiration.Unsubscribe(DeviceOnDataReceived.OnRespirationReceived);
            DeviceSettings.Instance.settings.Respiration = false;
        }
    }
    public void ReceiveRespirationRate(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.respirationRate.Subscribe(DeviceOnDataReceived.OnRespirationRateReceived);
            DeviceSettings.Instance.settings.RespirationRate = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.respirationRate.Unsubscribe(DeviceOnDataReceived.OnRespirationRateReceived);
            DeviceSettings.Instance.settings.RespirationRate = false;
        }
    }
    public void ReceiveRR(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.rr.Subscribe(DeviceOnDataReceived.OnRRReceived);
            DeviceSettings.Instance.settings.RR = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.rr.Unsubscribe(DeviceOnDataReceived.OnRRReceived);
            DeviceSettings.Instance.settings.RR = false;
        }
    }
    public void ReceivePressure(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.pressure.Subscribe(DeviceOnDataReceived.OnPressureReceived);
            DeviceSettings.Instance.settings.Pressure = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.pressure.Unsubscribe(DeviceOnDataReceived.OnPressureReceived);
            DeviceSettings.Instance.settings.Pressure = false;
        }
    }
    public void ReceiveOrientation(bool doReceive)
    {
        if (doReceive)
        {
            Aidlab.AidlabSDK.aidlabDelegate.orientation.Subscribe(DeviceOnDataReceived.OnOrientationReceived);
            DeviceSettings.Instance.settings.Orientation = true;
        }
        else
        {
            Aidlab.AidlabSDK.aidlabDelegate.orientation.Unsubscribe(DeviceOnDataReceived.OnOrientationReceived);
            DeviceSettings.Instance.settings.Orientation = false;
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
            DeviceSettings.Instance.settings.PlotECG = true;
        }
        else
        {
            GameObject FloatPlotter = GameObject.Find("FloatPlotter");
            if (FloatPlotter)
                GameObject.Destroy(FloatPlotter);
            Toggle toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
            toggle.interactable = false;
            DeviceSettings.Instance.settings.PlotECG = false;
        }
    }
    public void EnablePlottingDetectedPeaks(bool doReceive)
    {
        GameObject FloatPlotter = GameObject.Find("FloatPlotter");
        var FloatGraphComponent = FloatPlotter.GetComponent<FloatGraph>();
        if (doReceive)
        {
            FloatGraphComponent.showDetectedPoitns = true;
            DeviceSettings.Instance.settings.PlotDetectedPeaks = true;
        }
        else
        {
            FloatGraphComponent.showDetectedPoitns = false;
            DeviceSettings.Instance.settings.PlotDetectedPeaks = false;
        }
    }
}
