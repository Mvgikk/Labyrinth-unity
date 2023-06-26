using UnityEngine;
using UnityEngine.UI;

public class DeviceController : MonoBehaviour
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
            toggle.onValueChanged.AddListener(ReceiveWearState);
        
        toggle = HeartRateToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveHR);
        
        toggle = TemperatureToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveTemperature);
        
        toggle = RespirationToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveRespiration);
        
        toggle = RespirationRateToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveRespirationRate);
        
        toggle = RRToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveRR);
        
        toggle = PressureToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceivePressure);
        
        toggle = OrientationToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(ReceiveOrientation);
        
        toggle = ECGPlotToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(EnablePlotting);
        
        toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
        if (toggle != null)
            toggle.onValueChanged.AddListener(EnablePlottingDetectedPeaks);
        
        #endregion Toggles
        
    }

    public void ReceiveWearState(bool doReceive)
    {
        if (doReceive)
            Aidlab.AidlabSDK.aidlabDelegate.wearState.Subscribe(DeviceOnDataReceived.OnWearStateReceived);
        else
            Aidlab.AidlabSDK.aidlabDelegate.wearState.Unsubscribe(DeviceOnDataReceived.OnWearStateReceived);
    }
    public void ReceiveHR(bool doReceive)
    {
        if(doReceive)
        {
            GameObject ECGReceiver = new GameObject("ECGReceiver");
            GameObject.DontDestroyOnLoad(ECGReceiver);

            ECGReceiver.AddComponent<ECGReceiver>();
        }
        else
        {
            GameObject ECGReceiver = GameObject.Find("ECGReceiver");
            if(ECGReceiver)
                GameObject.Destroy(ECGReceiver);
        }
    }
    public void ReceiveTemperature(bool doReceive)
    {
        if (doReceive)
            Aidlab.AidlabSDK.aidlabDelegate.temperature.Subscribe(DeviceOnDataReceived.OnTemperatureReceived);
        else
            Aidlab.AidlabSDK.aidlabDelegate.temperature.Unsubscribe(DeviceOnDataReceived.OnTemperatureReceived);
    } 
    public void ReceiveRespiration(bool doReceive)
    {
        if (doReceive)
            Aidlab.AidlabSDK.aidlabDelegate.respiration.Subscribe(DeviceOnDataReceived.OnRespirationReceived);
        else
            Aidlab.AidlabSDK.aidlabDelegate.respiration.Unsubscribe(DeviceOnDataReceived.OnRespirationReceived);
    } 
    public void ReceiveRespirationRate(bool doReceive)
    {
        if (doReceive)
            Aidlab.AidlabSDK.aidlabDelegate.respirationRate.Subscribe(DeviceOnDataReceived.OnRespirationRateReceived);
        else
            Aidlab.AidlabSDK.aidlabDelegate.respirationRate.Unsubscribe(DeviceOnDataReceived.OnRespirationRateReceived);
    }
    public void ReceiveRR(bool doReceive)
    {
        if (doReceive)
            Aidlab.AidlabSDK.aidlabDelegate.rr.Subscribe(DeviceOnDataReceived.OnRRReceived);
        else
            Aidlab.AidlabSDK.aidlabDelegate.rr.Unsubscribe(DeviceOnDataReceived.OnRRReceived);
    } 
    public void ReceivePressure(bool doReceive)
    {
        if (doReceive)
            Aidlab.AidlabSDK.aidlabDelegate.pressure.Subscribe(DeviceOnDataReceived.OnPressureReceived);
        else
            Aidlab.AidlabSDK.aidlabDelegate.pressure.Unsubscribe(DeviceOnDataReceived.OnPressureReceived);
    } 
    public void ReceiveOrientation(bool doReceive)
    {
        if (doReceive)
            Aidlab.AidlabSDK.aidlabDelegate.orientation.Subscribe(DeviceOnDataReceived.OnOrientationReceived);
        else
            Aidlab.AidlabSDK.aidlabDelegate.orientation.Unsubscribe(DeviceOnDataReceived.OnOrientationReceived);
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
        }
        else
        {
            GameObject FloatPlotter = GameObject.Find("FloatPlotter");
            if (FloatPlotter)
                GameObject.Destroy(FloatPlotter);
            Toggle toggle = ECGPeaksDotsToggle.GetComponent<Toggle>();
            toggle.interactable = false;
        }
    }
    public void EnablePlottingDetectedPeaks(bool doReceive)
    {
        GameObject FloatPlotter = GameObject.Find("FloatPlotter");
        var FloatGraphComponent = FloatPlotter.GetComponent<FloatGraph>();
        if (doReceive) FloatGraphComponent.showDetectedPoitns = true;
        else FloatGraphComponent.showDetectedPoitns = false;
    }
}
