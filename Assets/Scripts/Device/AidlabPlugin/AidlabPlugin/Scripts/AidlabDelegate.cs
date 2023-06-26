using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System;

namespace Aidlab
{
    public class AidlabDelegate
    {
        public Data.Data1<float> ecg = new Data.Data1<float>("ECG");
        public Data.Data1<float> respiration = new Data.Data1<float>("Respiration");
        public Data.Data1<float> temperature = new Data.Data1<float>("Temperature");
        public Data.Data1<int> battery = new Data.Data1<int>("Battery");
        public Data.Data1<ActivityType> activity = new Data.Data1<ActivityType>("Activity");
        public Data.Data1<System.UInt64> steps = new Data.Data1<System.UInt64>("Steps");
        public Data.Data1<System.UInt32> respirationRate = new Data.Data1<System.UInt32>("Respiration Rate");
        public Data.Data1<WearState> wearState = new Data.Data1<WearState>("Wear State");
        public Data.Data1<int> heartRate = new Data.Data1<int>("Heart Rate");
        public Data.Data1<int> rr = new Data.Data1<int>("RR");
        public Data.Data1<System.UInt16> soundVolume = new Data.Data1<System.UInt16>("Sound Volume");
        public Data.Data1<Exercise> exercise = new Data.Data1<Exercise>("Exercise");
        public Data.Data3<float> accelerometer = new Data.Data3<float>("Accelerometer");
        public Data.Data3<float> gyroscope = new Data.Data3<float>("Gyroscope");
        public Data.Data3<float> magnetometer = new Data.Data3<float>("Magnetometer");
        public Data.Data3<float> orientation = new Data.Data3<float>("Orientation");
        public Data.Data1<BodyPosition> bodyPosition = new Data.Data1<BodyPosition>("Body Position");
        public Data.Data4<float, float> quaternion = new Data.Data4<float, float>("Quaternion");
        public Data.Data1<int> pressure = new Data.Data1<int>("Pressure");
        public Data.Data1<WearState> pressureWearState = new Data.Data1<WearState>("Pressure Wear State");



        public UnityEvent onDataReceivedEvents = new UnityEvent();
        public UnityEvent onAccelerometerDataReceivedEvents = new UnityEvent();
        protected void AfterDataReceived()
        {
            MainThreadWorker.ExecuteOnMainThread.Enqueue(onDataReceivedEvents);
        }


        #region ECG
        public Queue<double> signal = new Queue<double>();
        public Queue<System.UInt64> times = new Queue<System.UInt64>();

        // SEND DATA EVERY timeStamp SECONDS
        // timeStamp = -1   - DON'T RECEIVE DATA
        public int timeStamp = -1;
        System.Int64 timer = 0;
        #endregion ECG

        #region ACCELEROMETER
        public Vector3 accelerationXYZ;
        #endregion ACCELEROMETER

        public AidlabDelegate() {}

        // time IN SECS
        public void SetTimer(int timeStamp)
        {
            this.timeStamp = timeStamp;
            DateTimeOffset now = (DateTimeOffset)DateTime.UtcNow;
            timer = (System.Int64)now.ToUnixTimeSeconds() + (System.Int64)timeStamp;
        }

        public bool calibrated = false;
        public void DidReceiveECG(System.UInt64 timestamp, float value)
        {
            if (timeStamp == -1)
                return;
            if (timer - (System.Int64)((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds() > 0)
            {
                if (calibrated)
                {
                    signal.Dequeue();
                    signal.Enqueue(value);
                }
                else
                    signal.Enqueue(value);
                times.Enqueue(timestamp);
            }
            else
            {
                MainThreadWorker.ExecuteOnMainThread.Enqueue(onDataReceivedEvents);
                SetTimer(this.timeStamp);
                // values.Clear();
            }
            // AfterDataReceived();
        }

        public Queue<float> respirationSignal = new Queue<float>();
        public void DidReceiveRespiration(System.UInt64 timestamp, float value)
        {
            respirationSignal.Enqueue(value);
            respiration.ReceiveData(value, timestamp);
        }

        public void DidReceiveRespirationRate(System.UInt64 timestamp, System.UInt32 value)
        {
            respirationRate.ReceiveData(value, timestamp);
        }

        public void DidReceiveBatteryLevel(int stateOfCharge)
        {
            battery.ReceiveData(stateOfCharge, 0);
        }

        public void DidReceiveSkinTemperature(System.UInt64 timestamp, float value)
        {
            temperature.ReceiveData(value, timestamp);
        }

        public void DidReceiveAccelerometer(System.UInt64 timestamp, float ax, float ay, float az)
        {
            accelerationXYZ = new Vector3(ax, ay, az);
            MainThreadWorker.ExecuteOnMainThread.Enqueue(onAccelerometerDataReceivedEvents);
            // accelerometer.ReceiveData(ax, ay, az, timestamp);
        }

        public void DidReceiveGyroscope(System.UInt64 timestamp, float qx, float qy, float qz)
        {
            gyroscope.ReceiveData(qx, qy, qz, timestamp);
        }

        public void DidReceiveMagnetometer(System.UInt64 timestamp, float mx, float my, float mz)
        {
            magnetometer.ReceiveData(mx, my, mz, timestamp);
        }

        public void DidReceiveQuaternion(System.UInt64 timestamp, float qx, float qy, float qz, float qw)
        {
            quaternion.ReceiveData(qw, qx, qy, qz, timestamp);
        }

        public void DidReceiveOrientation(System.UInt64 timestamp, float roll, float pitch, float yaw)
        {
            orientation.ReceiveData(roll, pitch, yaw, timestamp);
        }

        public void DidReceiveBodyPosition(System.UInt64 timestamp, BodyPosition bodyPosition)
        {
            this.bodyPosition.ReceiveData(bodyPosition, timestamp);
        }

        public void DidReceiveActivity(System.UInt64 timestamp, ActivityType activity)
        {
            this.activity.ReceiveData(activity, timestamp);
        }

        public void DidReceiveSteps(System.UInt64 timestamp, System.UInt64 steps)
        {
            this.steps.ReceiveData(steps, timestamp);
        }

        public void DidReceiveHeartRate(System.UInt64 timestamp, int heartRate)
        {
            this.heartRate.ReceiveData(heartRate, timestamp);
        }

        public void DidReceiveRr(System.UInt64 timestamp, int rr)
        {
            this.rr.ReceiveData(rr, timestamp);
        }

        public void WearStateDidChange(WearState wearState)
        {
            this.wearState.ReceiveData(wearState, 0);
        }

        public void DidDetectExercise(Exercise exercise)
        {
            this.exercise.ReceiveData(exercise, 0);
        }

        public void DidReceiveSoundVolume(System.UInt64 timestamp, System.UInt16 value)
        {
            soundVolume.ReceiveData(value, timestamp);
        }

        public void DidReceivePressure(System.UInt64 timestamp, int pressure)
        {
            this.pressure.ReceiveData(pressure, timestamp);
        }

        public void PressureWearStateDidChange(WearState wearState)
        {
            pressureWearState.ReceiveData(wearState, 0);
        }
    }
}
