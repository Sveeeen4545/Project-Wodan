
using UnityEngine;
using Unity.Netcode;
using Unity.Burst.CompilerServices;
using System.Collections.Generic;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

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
        Firetruck,
        Broken_Car,
        Cars_light_multiple,
        Cars_light_single,
        Bikes_light,
        House_blue,
        House_brown,
        House_White,
        Iron_fence,
        Iron_fence_half,
        Street_light,
        Street_light_double,
        Tree,
        Generator
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
                    "Stuck and Communicative",
                    "Adult",
                    true
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
                SpawnByIndex(1, pos, rot);
                break;

            case SpawnTypes.Firetruck:
                SpawnByIndex(2, pos, rot);
                break;

            case SpawnTypes.Broken_Car:
                SpawnByIndex(3, pos, rot);
                break;

            case SpawnTypes.Cars_light_multiple:
                SpawnByIndex(4, pos, rot);
                break;

            case SpawnTypes.Cars_light_single:
                SpawnByIndex(5, pos, rot);
                break;

            case SpawnTypes.Bikes_light:
                SpawnByIndex(6, pos, rot);
                break;

            case SpawnTypes.House_blue:
                SpawnByIndex(7, pos, rot);
                break;

            case SpawnTypes.House_brown:
                SpawnByIndex(8, pos, rot);
                break;

            case SpawnTypes.House_White:
                SpawnByIndex(9, pos, rot);
                break;

            case SpawnTypes.Iron_fence:
                SpawnByIndex(10, pos, rot);
                break;

            case SpawnTypes.Iron_fence_half:
                SpawnByIndex(11, pos, rot);
                break;

            case SpawnTypes.Street_light:
                SpawnByIndex(12, pos, rot);
                break;

            case SpawnTypes.Street_light_double:
                SpawnByIndex(13, pos, rot);
                break;
            case SpawnTypes.Tree:
                SpawnByIndex(14, pos, rot);
                break;
            case SpawnTypes.Generator:
                SpawnByIndex(15, pos, rot);
                break;

            default: Debug.Log("ERROR: unknown type");
                break;
        }
    }

    private void SpawnByIndex(int index, Vector3 pos, Quaternion rot)
    {
        obj = Instantiate(networkPrefab[index], pos, rot);
        obj.GetComponent<NetworkObject>().Spawn();
    }


}