using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolGizmoScript : MonoBehaviour
{
    private Transform[] waypoints;
    // Start is called before the first frame update
    void Start()
    {
        var waypointsList = gameObject.GetComponentsInChildren<Transform>().Skip(1);

        waypoints = waypointsList.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        Gizmos.color = Color.cyan;
        //Gizmos.DrawLineList(points);
    }

    public Transform[] GetWaypoints()
    {
        return waypoints;
    }

}
