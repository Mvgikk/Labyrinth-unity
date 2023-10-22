using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HeartRateGenerator : MonoBehaviour
{
    [SerializeField]
    int MinValue = 60;

    [SerializeField]
    private int MaxValue = 100;

    [SerializeField]
    int MaxOffset = 10;

    [SerializeField]
    int hrValue;

    private float timeSinceLastUpdate = 0.0f;
    private UnityEvent<int> receivedHR;

    void Start()
    {
        hrValue = Random.Range(MinValue, MaxValue);
        DontDestroyOnLoad(this);

        /*
         * Remove existing device receiving object.
         */
        var ecgReceiver = GameObject.Find("ECGReceiver");
        if (ecgReceiver != null)
            Destroy(ecgReceiver);

        /* 
         * Rename this game object to "ECGReceiver"
         * It makes that we don't have to change name of game object in scripts
         * in receiving heart rate measurement.
         */
        this.gameObject.name = "ECGReceiver";

        var ecgReceiverScript = GetComponent<ECGReceiver>();
        if (ecgReceiverScript != null)
        {
            receivedHR = ecgReceiverScript.receivedHR;
            return;
        }

        this.gameObject.SetActive(false);
        Debug.LogError($"{GetType().Name}: Didn't found ECG Receiver script.");
    }

    void Update()
    {
        timeSinceLastUpdate += Time.deltaTime;

        // RECEIVE HR EVERY 1 SEC
        if(timeSinceLastUpdate >= 1.0f)
        {
            GenerateHR();
            timeSinceLastUpdate = 0.0f;
        }
    }

    void GenerateHR()
    {
        int value;
        if (MaxOffset > 0)
        {
            do
            {
                value = Random.Range(-MaxOffset / 2, MaxOffset / 2);
            } while (!(hrValue + value > MinValue && hrValue + value < MaxValue));
            hrValue += value;
        }
        receivedHR.Invoke(hrValue);
    }
}
