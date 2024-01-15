
using UnityEngine;
using Unity.Netcode;
using Unity.Burst.CompilerServices;

public class NetworkSpawner : NetworkBehaviour
{
    public GameObject networkPrefab; 

    public enum SpawnTypes
    {
        Empy,
        Victim,
        Hazard 
    }


    [ServerRpc(RequireOwnership = false)]
    public void RequestSpawnServerRpc( Vector3 pos,Quaternion rot, SpawnTypes type,  ServerRpcParams serverRpcParams = default)
    {
        GameObject obj = Instantiate(networkPrefab, pos, rot);
        obj.GetComponent<NetworkObject>().Spawn();

        switch (type)
        { 
            case SpawnTypes.Empy:
                break; 

            case SpawnTypes.Victim:
                // add victim 
                CanvasHandeler.instance.sceneData.AddVictim(
                    obj.GetComponent<NetworkObject>().NetworkObjectId,
                    60f,
                    "This victim was succesfully created by yaboi",
                    "its so old cuz it took so long",
                    false
                    );
                break;

           case SpawnTypes.Hazard:
                CanvasHandeler.instance.sceneData.AddHazard(
                    obj.GetComponent<NetworkObject>().NetworkObjectId,
                    60f,
                    Hazard.HazardTypes.Fire
                    );
                break; 

            default: Debug.Log("ERROR: unknown type");
                break; 
        }
    }
}