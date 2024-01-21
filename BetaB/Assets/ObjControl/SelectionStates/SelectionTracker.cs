using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static NetworkSpawner;

public class SelectionTracker : MonoBehaviour
{
    public GameObject[] HazardEffects = new GameObject[5];

    //public List<GameObject> Spawns = new List<GameObject>();


    public GameObject dropperPrefab;
    private SceneObjControl _selection;

    private Animator animator;

    public NetworkSpawner.SpawnTypes spawntype;

    public SceneObjControl Selection
    {
        get { return _selection; }
    }

    public void SetSelection(SceneObjControl newSelection)
    {
        _selection = newSelection;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void SetState(int state)
    {
        animator.SetInteger("SelectionState", state);
    }

    public void SetSpawnIndex(int index)
    {
        switch (index)
        {
            case 0: spawntype = SpawnTypes.Empy; break;
            case 1:spawntype = SpawnTypes.Car; break;
            case 2: spawntype = SpawnTypes.Firetruck; break;
            case 3:spawntype = SpawnTypes.Broken_Car ;break;
            case 4: spawntype = SpawnTypes.Cars_light_multiple ; break;
            case 5: spawntype = SpawnTypes.Cars_light_single ; break;
            case 6: spawntype = SpawnTypes.Bikes_light ; break;
            case 7: spawntype = SpawnTypes.House_blue ; break;
            case 8: spawntype = SpawnTypes.House_brown ; break;
            case 9: spawntype = SpawnTypes.House_White ; break;
            case 10: spawntype = SpawnTypes.Iron_fence ; break;
            case 11: spawntype = SpawnTypes.Iron_fence_half ; break;
            case 12: spawntype = SpawnTypes.Street_light ; break;
            case 13: spawntype = SpawnTypes.Street_light_double ; break;
            case 14: spawntype = SpawnTypes.Tree ; break;
            case 15: spawntype = SpawnTypes.Generator ; break;
            case 16: spawntype = SpawnTypes.Spreader ; break;
            case 17: spawntype = SpawnTypes.Cutter; break;

            default: spawntype = SpawnTypes.Empy; break; 

        }
    }

    public void Delete()
    {
        Selection.RequestRemoval();
        animator.SetInteger("SelectionState", 0);
    }

    //[ServerRpc(RequireOwnership = false)]
    //public void RequestSpawnServerRpc(int index, Vector3 pos, Quaternion rot , ServerRpcParams serverRpcParams = default)
    //{

    //    //Instantiate(Spawns[0], pos, rot).Spawn();
    //    GameObject obj = Instantiate(Spawns[index] , pos, rot);
    //    obj.GetComponent<NetworkObject>().Spawn();
    //}

}

