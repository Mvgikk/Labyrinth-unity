using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.ParametersController
{
    public class ParametersStats : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI wearStateTMP;
        [SerializeField]
        private TextMeshProUGUI heartRateTMP;
        [SerializeField]
        private TextMeshProUGUI temperatureTMP;
        [SerializeField]
        private TextMeshProUGUI respirationTMP;
        [SerializeField]
        private TextMeshProUGUI respirationRateTMP;
        [SerializeField]
        private TextMeshProUGUI RRTMP;
        [SerializeField]
        private TextMeshProUGUI pressureTMP;
        [SerializeField]
        private TextMeshProUGUI orientationTMP;

        /*
         * signalize if function is subscribed by SDK value receiver
         */
        #region CHECKERS
        private bool wearStateHandler = false;
        private bool heartRateHandler = false;
        private bool temperatureHandler = false;
        private bool respirationHandler = false;
        private bool respirationRateHandler = false;
        private bool RRHandler = false;
        private bool pressureHandler = false;
        private bool orientationHandler = false;
        #endregion CHECKERS

        private void Start()
        {
            if (!wearStateTMP || !heartRateTMP || !temperatureTMP || !respirationTMP || !respirationRateTMP || !RRTMP || !pressureTMP || !orientationTMP) 
            {
                Debug.LogError("Didn't attached textMeshPro component in ParametersStats.cs");
                Destroy(this.gameObject);
                return;
            }

            StartCoroutine(checkForSettingsUpdate());
        }

        /*
         * checks if there are unused text objects, if so, clear text
         * NOTE: we don't delete tmp because we can use them after disable setting
         * NOTE2: we don't need to unsubscribe methods, bcs after change setting to disable, it calls unsubcribeAll
         */
        IEnumerator checkForSettingsUpdate()
        {
            while (true)
            {
                if (DeviceSettings.Instance.settings.WearState == false)
                {
                    wearStateTMP.text = string.Empty;
                    wearStateHandler = false;
                }
                else
                {
                    if (wearStateHandler == false)
                    {
                        Aidlab.AidlabSDK.aidlabDelegate.wearState.Subscribe(DisplayWearState);
                        wearStateHandler = true;
                    }
                }

                if (DeviceSettings.Instance.settings.HeartRate == false)
                {
                    heartRateTMP.text = string.Empty;
                    heartRateHandler = false;
                }
                else
                {
                    if (heartRateHandler == false)
                    {
                        GameObject ECGReceiver = GameObject.Find("ECGReceiver");

                        ECGReceiver comp = ECGReceiver.GetComponent<ECGReceiver>();
                        comp.receivedHR.AddListener(DisplayHeartRate);
                        heartRateHandler = true;
                    }
                }

                if (DeviceSettings.Instance.settings.Temperature == false)
                {
                    temperatureTMP.text = string.Empty;
                    temperatureHandler = false;
                }
                else
                {
                    if (temperatureHandler == false)
                    {
                        Aidlab.AidlabSDK.aidlabDelegate.temperature.Subscribe(DisplayTemperature);
                        temperatureHandler = true;
                    }
                }

                if (DeviceSettings.Instance.settings.Respiration == false)
                {
                    respirationTMP.text = string.Empty;
                    respirationHandler = false;
                }
                else
                {
                    if (respirationHandler == false)
                    {
                        Aidlab.AidlabSDK.aidlabDelegate.respiration.Subscribe(DisplayRespiration);
                        respirationHandler = true;
                    }
                }

                if (DeviceSettings.Instance.settings.RespirationRate == false)
                {
                    respirationRateTMP.text = string.Empty;
                    respirationRateHandler = false;
                }
                else
                {
                    if (respirationRateHandler == false)
                    {
                        Aidlab.AidlabSDK.aidlabDelegate.respirationRate.Subscribe(DisplayRespirationRate);
                        respirationRateHandler = true;
                    }
                }

                if (DeviceSettings.Instance.settings.Pressure == false)
                {
                    pressureTMP.text = string.Empty;
                    pressureHandler = false;
                }
                else
                {
                    if (pressureHandler == false)
                    {
                        Aidlab.AidlabSDK.aidlabDelegate.pressure.Subscribe(DisplayPressure);
                        pressureHandler = true;
                    }
                }

                if (DeviceSettings.Instance.settings.RR == false)
                {
                    RRTMP.text = string.Empty;
                    RRHandler = false;
                }
                else
                {
                    if (RRHandler == false)
                    {
                        Aidlab.AidlabSDK.aidlabDelegate.rr.Subscribe(DisplayRR);
                        RRHandler = true;
                    }
                }

                if (DeviceSettings.Instance.settings.Orientation == false)
                {
                    orientationTMP.text = string.Empty;
                    orientationHandler = false;
                }
                else
                {
                    if (orientationHandler == false)
                    {
                        Aidlab.AidlabSDK.aidlabDelegate.orientation.Subscribe(DisplayOrientation);
                        orientationHandler = true;
                    }
                }

                yield return new WaitForSeconds(1f);
            }
        }

        public void DisplayWearState() { wearStateTMP.text = "Wear state: " + Aidlab.AidlabSDK.aidlabDelegate.wearState.value; }
        public void DisplayHeartRate(int hr) { heartRateTMP.text = "Heart rate: " + hr; }
        public void DisplayTemperature() { temperatureTMP.text = "Temperature: " + Aidlab.AidlabSDK.aidlabDelegate.temperature.value; }
        public void DisplayRespiration() { respirationTMP.text = "Respiration: " + Aidlab.AidlabSDK.aidlabDelegate.respiration.value; }
        public void DisplayRespirationRate() { respirationRateTMP.text = "Respiration rate: " + Aidlab.AidlabSDK.aidlabDelegate.respirationRate.value; }
        public void DisplayRR() { RRTMP.text = "RR: " + Aidlab.AidlabSDK.aidlabDelegate.rr.value; }
        public void DisplayPressure() { pressureTMP.text = "Pressure: " + Aidlab.AidlabSDK.aidlabDelegate.pressure.value; }
        public void DisplayOrientation() {
            orientationTMP.text = "Orientation: \n\tX: " + Aidlab.AidlabSDK.aidlabDelegate.orientation.x + "\tY: " + Aidlab.AidlabSDK.aidlabDelegate.orientation.y + "" +
                "\tZ: " + Aidlab.AidlabSDK.aidlabDelegate.orientation.z;
        }
    }
}
