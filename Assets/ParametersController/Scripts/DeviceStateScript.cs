using Aidlab;
using Aidlab.BLE;
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
        MainThreadDispatcher.Instance.EnsureInitialized();
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
        // Modify UI element from the main thread
        if (deviceState == BLEStatus.ScanningDevices)
            MainThreadDispatcher.Instance.Enqueue(() =>
            {
                controll.color = new Color32(255, 255, 255, 255);   // SCANNING -> white
            });

        else if (deviceState == BLEStatus.TryingToConnect)
            MainThreadDispatcher.Instance.Enqueue(() =>
            {
                controll.color = new Color32(139, 255, 255, 255);
            });
        else if (deviceState == BLEStatus.Connected)
        {
            if (wearState == WearState.Detached)
            MainThreadDispatcher.Instance.Enqueue(() =>
            {
                controll.color = new Color32(0, 243, 255, 255); // CONNECTED & NOT WEARED -> blue
            });
            else if (wearState == WearState.PlacedProperly)
            MainThreadDispatcher.Instance.Enqueue(() =>
            {
                controll.color = new Color32(33, 255, 0, 255);  // CONNECTED & WEARED -> green
            });

        }
        else if (deviceState == BLEStatus.None)
        MainThreadDispatcher.Instance.Enqueue(() =>
        {
            controll.color = new Color32(80, 80, 80, 255);      // DISABLED | None -> gray
        });
        else
        MainThreadDispatcher.Instance.Enqueue(() =>
        {
            controll.color = new Color32(147, 0, 0, 255);       // UNKNOWN STATE
        });


    }

    [SerializeField]
    public UnityEngine.Object parametersSettingsPrefab;
    public void OnPointerClick(PointerEventData eventData)
    {
        //GameObject root = gameObject.transform.parent.gameObject;
        //GameObject settingsObj = root.transform.Find("ParametersSettingsPrefab").gameObject;
        //if (settingsObj == null)
        //{
        //    Debug.Log("NULL");
        //    Object prefab = parametersSettingsPrefab;
        //    settingsObj = Instantiate(prefab) as GameObject;
        //    settingsObj.name = "ParametersSettingsPrefab";
        //}
        //if (settingsObj.active)
        //    MainThreadDispatcher.Instance.Enqueue(() =>
        //    {
        //        settingsObj.SetActive(false);
        //    });
        //else
        //    MainThreadDispatcher.Instance.Enqueue(() =>
        //    {
        //        settingsObj.SetActive(true);
        //    });
    }
}
