using Aidlab;
using Aidlab.BLE;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeviceStateScript : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Image controll;

    private BLEStatus deviceState = BLEStatus.None;
    private BLEStatus DeviceState
    {
        get { return deviceState; }
        set
        {
            deviceState = value;
            SetColor();
        }
    }

    private WearState wearState = WearState.Detached;
    private WearState WearState
    {
        get { return wearState; }
        set
        {
            wearState = value;
            SetColor();
        }
    }

    void Start()
    {
        BLEConnector.deviceStatusChanged.AddListener(SetState);
        AidlabDelegate.wearStateChanged.AddListener(GetWearStateEvent);
    }

    public void SetState(BLEStatus state)
    {
        DeviceState = state;
        if(deviceState == BLEStatus.None || deviceState == BLEStatus.ScanningDevices || deviceState == BLEStatus.TryingToConnect) 
        {
            WearState = WearState.Detached;
        }
    }

    public void GetWearStateEvent()
    {
        SetState(Aidlab.AidlabSDK.aidlabDelegate.wearState.value);
    }
    public void SetState(WearState state){ WearState = state; }

    private void SetColor() 
    {
        if (deviceState == BLEStatus.ScanningDevices)
            controll.color = new Color32(255, 255, 255, 255);   // SCANNING -> white
        else if(deviceState == BLEStatus.TryingToConnect)
            controll.color = new Color32(139, 255, 255, 255);
        else if (deviceState == BLEStatus.Connected)
        {
            if (wearState == WearState.Detached) 
                controll.color = new Color32(0, 243, 255, 255); // CONNECTED & NOT WEARED -> blue
            else if (wearState == WearState.PlacedProperly)
                controll.color = new Color32(33, 255, 0, 255);  // CONNECTED & WEARED -> green
        }
        else if (deviceState == BLEStatus.None)
            controll.color = new Color32(80, 80, 80, 255);      // DISABLED | None -> gray
        else
            controll.color = new Color32(147, 0, 0, 255);       // UNKNOWN STATE
    }

    [SerializeField]
    public UnityEngine.Object deviceSettingsPrefab;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject root = gameObject.transform.parent.gameObject;
        GameObject settingsObj = root.transform.Find("ParametersSettingsPrefab").gameObject;
        if (settingsObj == null)
        {
            Debug.Log("NULL");
            Object prefab = deviceSettingsPrefab;
            settingsObj = Instantiate(prefab) as GameObject;
            settingsObj.name = "ParametersSettingsPrefab";
        }
        if(settingsObj.active)
            settingsObj.SetActive(false);
        else
            settingsObj.SetActive(true);
    }
}
