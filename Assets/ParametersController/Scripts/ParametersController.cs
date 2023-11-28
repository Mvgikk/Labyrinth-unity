using Assets.Scripts.ParametersController;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ParametersController : MonoBehaviour
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
    [SerializeField] GameObject ShowValuesToggle;

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
        
        toggle = ShowValuesToggle.GetComponent<Toggle>();
        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(DisplayParametersValues);
            toggle.isOn = DeviceSettings.Instance.settings.ShowValues;
        }
        gameObject.SetActive(false);
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
            Aidlab.AidlabSDK.aidlabDelegate.wearState.UnsubscribeAll();
            DeviceSettings.Instance.settings.WearState = false;
        }
    }
    public void ReceiveHR(bool doReceive)
    {
        if (doReceive)
        {
            GameObject ECGReceiver = new GameObject("ECGReceiver");
            GameObject.DontDestroyOnLoad(ECGReceiver);

            ECGReceiver comp = ECGReceiver.AddComponent<ECGReceiver>();
            comp.receivedHR.AddListener(DeviceOnDataReceived.OnHeartRateReceived);
            DeviceSettings.Instance.settings.HeartRate = true;
        }
        else
        {
            GameObject ECGReceiver = GameObject.Find("ECGReceiver");
            if (ECGReceiver)
            {
                ECGReceiver.GetComponent<ECGReceiver>().receivedHR.RemoveAllListeners();
                GameObject.Destroy(ECGReceiver);
            }
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
            Aidlab.AidlabSDK.aidlabDelegate.temperature.UnsubscribeAll();
            // Aidlab.AidlabSDK.aidlabDelegate.temperature.Unsubscribe(DeviceOnDataReceived.OnTemperatureReceived);
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
            Aidlab.AidlabSDK.aidlabDelegate.respiration.UnsubscribeAll();
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
            Aidlab.AidlabSDK.aidlabDelegate.respirationRate.UnsubscribeAll();
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
            Aidlab.AidlabSDK.aidlabDelegate.rr.UnsubscribeAll();
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
            Aidlab.AidlabSDK.aidlabDelegate.pressure.UnsubscribeAll();
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
            Aidlab.AidlabSDK.aidlabDelegate.orientation.UnsubscribeAll();
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
            // Toggle toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
            // toggle.interactable = true;
            DeviceSettings.Instance.settings.PlotECG = true;
        }
        else
        {
            GameObject FloatPlotter = GameObject.Find("FloatPlotter");
            if (FloatPlotter)
                GameObject.Destroy(FloatPlotter);
            // Toggle toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
            // toggle.interactable = false;
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


    [SerializeField]
    public UnityEngine.Object ParametersValuesPrefab;
    public void DisplayParametersValues(bool doReceive)
    {
        if (doReceive)
        {
            DeviceSettings.Instance.settings.ShowValues = true;
            GameObject parametersValues = GameObject.Find("ParametersValues");
            if (parametersValues == null)
            {
                UnityEngine.Object prefab = ParametersValuesPrefab;
                parametersValues = Instantiate(prefab) as GameObject;
                parametersValues.name = parametersValues.name.Replace("(Clone)", "").Trim();
                DontDestroyOnLoad(parametersValues);
            }
            ParametersStats parametersStats = parametersValues.GetComponent<ParametersStats>();
            if (parametersStats == null) 
            {
                UnityEngine.Debug.LogError("ParametersValues object don't have ParametersStats component");
                Destroy(parametersValues);
                return;
            }
            //Aidlab.AidlabSDK.aidlabDelegate.wearState.Subscribe(parametersStats.Display);
        }
        else
        {
            GameObject parametersValues = GameObject.Find("ParametersValues");
            Destroy(parametersValues);
            DeviceSettings.Instance.settings.ShowValues = false;
        }
    }
}
