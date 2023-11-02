using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationSettings : MonoBehaviour
{
    public static bool isSimulated;
    public static SimulationLevel simulationLevel;
}

public enum SimulationLevel
{
    Low,
    Medium,
    High
}
