
using UnityEngine;
using Unity.Netcode;

public class NetworkSpawner : NetworkBehaviour
{
    public GameObject networkPrefab; 

    [ServerRpc(RequireOwnership = false)]
    public void RequestSpawnServerRpc( Vector3 pos,Quaternion rot,  ServerRpcParams serverRpcParams = default)
    {
        Instantiate(networkPrefab, pos, rot).GetComponent<NetworkObject>().Spawn();
    }
}