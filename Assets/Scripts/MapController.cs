using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject map;
    public GameObject mapLight;
    public bool isMapVisible = false;
    public bool hasCollectedAllScrolls = false;

    private void Start()
    {
    }

    private void Update()
    {
        if(hasCollectedAllScrolls)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMinimap(!isMapVisible);
            }
        }



    }

    private void ToggleMinimap(bool showMap)
    {
        if(hasCollectedAllScrolls)
        {
            map.SetActive(showMap);
            mapLight.SetActive(showMap);
            isMapVisible = showMap;
        }

    }
}
