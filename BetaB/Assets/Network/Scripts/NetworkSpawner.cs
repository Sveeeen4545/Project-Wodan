
using UnityEngine;
using Unity.Netcode;
using Unity.Burst.CompilerServices;
using System.Collections.Generic;

public class NetworkSpawner : NetworkBehaviour
{
    public List<GameObject> networkPrefab = new List<GameObject>();

    private GameObject obj; 


    public enum SpawnTypes
    {
        Empy,
        Victim,
        Hazard,
        Car,
        Firetruck
    }


    [ServerRpc(RequireOwnership = false)]
    public void RequestSpawnServerRpc( Vector3 pos,Quaternion rot, SpawnTypes type,  ServerRpcParams serverRpcParams = default)
    {
        //GameObject obj = Instantiate(networkPrefab, pos, rot);
        //obj.GetComponent<NetworkObject>().Spawn();

        switch (type)
        { 
            case SpawnTypes.Empy:
                break; 

            case SpawnTypes.Victim:

                obj = Instantiate(networkPrefab[0], pos, rot);
                obj.GetComponent<NetworkObject>().Spawn();

                CanvasHandeler.instance.sceneData.AddVictim(
                    obj.GetComponent<NetworkObject>().NetworkObjectId,
                    60f,
                    "This victim was succesfully created by yaboi",
                    "its so old cuz it took so long",
                    false
                    );
                break;

           case SpawnTypes.Hazard:

                obj = Instantiate(networkPrefab[0], pos, rot);
                obj.GetComponent<NetworkObject>().Spawn();

                CanvasHandeler.instance.sceneData.AddHazard(
                    obj.GetComponent<NetworkObject>().NetworkObjectId,
                    60f,
                    Hazard.HazardTypes.Fire
                    );
                break;

            case SpawnTypes.Car:
                obj = Instantiate(networkPrefab[1], pos, rot);
                obj.GetComponent<NetworkObject>().Spawn();
                break;

            case SpawnTypes.Firetruck:
                obj = Instantiate(networkPrefab[2], pos, rot);
                obj.GetComponent<NetworkObject>().Spawn();
                break;

            default: Debug.Log("ERROR: unknown type");
                break; 
        }
    }
}