using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SimulationSettings : MonoBehaviour
{
    public static bool isSimulated = false;
    public static SimulationLevel simulationLevel;
}

public enum SimulationLevel
{
    Low,
    Medium,
    High
}
