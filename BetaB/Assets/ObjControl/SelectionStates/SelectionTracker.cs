using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static NetworkSpawner;

public class SelectionTracker : MonoBehaviour
{
    public GameObject[] HazardEffects = new GameObject[5];

    public List<GameObject> Spawns = new List<GameObject>();


    public GameObject dropperPrefab;
    private SceneObjControl _selection;

    private Animator animator;

    private int networkindex;

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

    public void SetNetworkIndex(int index)
    {
        networkindex = index;
    }

    public void Delete()
    {
        Selection.RequestRemoval();
        animator.SetInteger("SelectionState", 0);
    }

    [ServerRpc(RequireOwnership = false)]
    public void RequestSpawnServerRpc(Vector3 pos, Quaternion rot , ServerRpcParams serverRpcParams = default)
    {
        GameObject obj = Instantiate(Spawns[networkindex] , pos, rot);
        obj.GetComponent<NetworkObject>().Spawn();
    }

}

