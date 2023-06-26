using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialScript : MonoBehaviour
{
    private static GameObject instance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = gameObject;
            Aidlab.AidlabSDK.init();
        }
        else
            Destroy(gameObject);
    }
}
