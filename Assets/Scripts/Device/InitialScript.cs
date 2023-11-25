using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialScript : MonoBehaviour
{
    private static GameObject instance;

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = gameObject;
            Aidlab.AidlabSDK.init();
            // Aidlab.AidlabSDK.aidlabDelegate.wearState.Subscribe(() => { Debug.Log(Aidlab.AidlabSDK.aidlabDelegate.wearState.value); });
        }
        else
            Destroy(gameObject);
    }
}
