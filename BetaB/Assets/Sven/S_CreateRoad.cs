using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static NetworkSpawner;

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


    public void RequestRoad(int roadType)
    {
        RoadPanel.SetActive(false);
        initialized = true;
        CanvasHandeler.instance.inventory.SetActive(true);
        switch (roadType)
        {
            case 0: Instantiate(S_SingleLane).GetComponent<NetworkObject>().Spawn(); break;
            case 1: Instantiate(S_DoubleLane).GetComponent<NetworkObject>().Spawn(); break;
            case 2: Instantiate(S_CurvedRoad).GetComponent<NetworkObject>().Spawn(); break;
            case 3: Instantiate(S_Intersection).GetComponent<NetworkObject>().Spawn(); break;
            default: Debug.Log("ERROR: unknown roadType"); break;
        }
    }

    // oops this was unneccasary 
    [ServerRpc]
    public void RequestRoadServerRpc(int roadType, ServerRpcParams serverRpcParams = default)
    {
        switch (roadType)
        {
            case 0: Instantiate(S_SingleLane).GetComponent<NetworkObject>().Spawn(); break;
            case 1: Instantiate(S_DoubleLane).GetComponent<NetworkObject>().Spawn(); break;
            case 2: Instantiate(S_CurvedRoad).GetComponent<NetworkObject>().Spawn(); break;
            case 3: Instantiate(S_Intersection).GetComponent<NetworkObject>().Spawn(); break; 
            default: Debug.Log("ERROR: unknown roadType"); break;
        }
    }

    //public void SpawnSingleLane()
    //{
    //    Instantiate(S_SingleLane).GetComponent<NetworkObject>().Spawn();
        

    //}
    //public void SpawnDoubleLane()
    //{
    //    Instantiate (S_DoubleLane).GetComponent<NetworkObject>().Spawn();
    //    RoadPanel.SetActive(false);
    //    initialized = true;
    //    CanvasHandeler.instance.inventory.SetActive(true);



    //}
    //public void SpawnCurvedRoad()
    //{
    //    Instantiate(S_CurvedRoad).GetComponent<NetworkObject>().Spawn();
    //    RoadPanel.SetActive(false);
    //    initialized = true;
    //    CanvasHandeler.instance.inventory.SetActive(true);

    //}
    //public void SpawnIntersection()
    //{
    //    Instantiate(S_Intersection).GetComponent<NetworkObject>().Spawn();
    //    RoadPanel.SetActive(false);
    //    initialized = true;
    //    CanvasHandeler.instance.inventory.SetActive(true);

    //}
}
