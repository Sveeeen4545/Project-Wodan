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
            default: break;

        }


        //spawntype = index;
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

