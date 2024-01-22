using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class S_CreateRoad : NetworkBehaviour
{
    [SerializeField] private GameObject S_SingleLane;
    [SerializeField] private GameObject S_DoubleLane;
    [SerializeField] private GameObject S_CurvedRoad;
    [SerializeField] private GameObject S_Intersection;

    [SerializeField] private GameObject RoadPanel;

    private bool initialized = false;

    private void Update()
    {

        if (!IsHost || initialized)
        {
            return; 
        }
        RoadPanel.SetActive(true);
        CanvasHandeler.instance.inventory.SetActive(false);

    }


    public void SpawnSingleLane()
    {
        Instantiate(S_SingleLane).GetComponent<NetworkObject>().Spawn();
        RoadPanel.SetActive(false);
        initialized = true;
        CanvasHandeler.instance.inventory.SetActive(true);

    }
    public void SpawnDoubleLane()
    {
        Instantiate (S_DoubleLane).GetComponent<NetworkObject>().Spawn();
        RoadPanel.SetActive(false);
        initialized = true;
        CanvasHandeler.instance.inventory.SetActive(true);



    }
    public void SpawnCurvedRoad()
    {
        Instantiate(S_CurvedRoad).GetComponent<NetworkObject>().Spawn();
        RoadPanel.SetActive(false);
        initialized = true;
        CanvasHandeler.instance.inventory.SetActive(true);

    }
    public void SpawnIntersection()
    {
        Instantiate(S_Intersection).GetComponent<NetworkObject>().Spawn();
        RoadPanel.SetActive(false);
        initialized = true;
        CanvasHandeler.instance.inventory.SetActive(true);

    }
}
