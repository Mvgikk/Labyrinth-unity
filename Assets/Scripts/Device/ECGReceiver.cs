using ECGtoHRcsharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ECGReceiver : MonoBehaviour
{
    public UnityEvent<int> receivedHR;

    [SerializeField]
    int ECGDataOffset = 2;              // Receive ECG data after 2 series (better data)

    [SerializeField]
    int ECGDataCalibrateOffset = 10;    // Time for calibration (read first X secs of ECG signal)
    int ECGSamplesDenominator;

    [SerializeField]
    int ECGDataDelay = 1;               // Receive data every X secs

    [SerializeField]
    GameObject floatGraphObject;

    private bool wearState = false;
    public FloatGraph floatGraph;
    private PanTompkins panTompkins;
    private List<double> ecgSignals;

    private void Awake()
    {
        // Aidlab.AidlabSDK.init();
    }

    // Start is called before the first frame update
    void Start()
    {
        panTompkins = new PanTompkins(248);
        ECGSamplesDenominator = ECGDataCalibrateOffset;

        Aidlab.AidlabSDK.aidlabDelegate.onDataReceivedEvents.AddListener(ReceivedECG);
        Aidlab.AidlabSDK.aidlabDelegate.SetTimer(ECGDataDelay);

        GameObject FloatPlotter = GameObject.Find("FloatPlotter");
        if (FloatPlotter != null)
            floatGraph = FloatPlotter.GetComponent<FloatGraph>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Aidlab.AidlabSDK.aidlabDelegate.onDataReceivedEvents.RemoveAllListeners();
        Aidlab.AidlabSDK.aidlabDelegate.SetTimer(-1);
        Aidlab.AidlabSDK.aidlabDelegate.calibrated = false;
    }

    private void ReceivedECG()
    {
        if (ECGDataOffset == 0)
        {
            if (!Aidlab.AidlabSDK.aidlabDelegate.calibrated)
            {
                if (--ECGDataCalibrateOffset == 0)
                {
                    Debug.Log("Koniec kalibracji");
                    Aidlab.AidlabSDK.aidlabDelegate.calibrated = true;
                    Aidlab.AidlabSDK.aidlabDelegate.SetTimer(ECGDataDelay);
                }
                else
                    Debug.Log("Kalibracja");
            }
            else
            {
                ecgSignals = new List<double>(Aidlab.AidlabSDK.aidlabDelegate.signal);
                var hr = panTompkins.detectPeaks(ecgSignals);
                // Debug.Log(hr);

                if (receivedHR != null)
                    receivedHR.Invoke((int)hr);

                if (floatGraph != null)
                {
                    List<int> x;
                    List<double> y;
                    panTompkins.getXYOfPeaks(ecgSignals, out x, out y);
                    floatGraph.SetDetectedPoints(x, y);
                    floatGraph.SetDataPoints(ecgSignals);
                }
            }
        }
        else
        {
            Debug.Log("Dataoffset");
            ECGDataOffset--;
            Aidlab.AidlabSDK.aidlabDelegate.signal.Clear();
        }
    }
}
