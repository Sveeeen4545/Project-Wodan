using Unity.Burst.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SceneObjControl : NetworkBehaviour
{
    public void RequestRemoval()
    {
        RequestRemovalServerRPC(); 
    }

    public void RequestDrag(Vector3 pos)
    {
        RequestDragServerRpc(pos);
    }

    public void RequestRotate(Vector3 pos)
    {
        RequestRotateServerRpc(pos);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestRemovalServerRPC(ServerRpcParams serverRpcParams = default)
    {
        GetComponent<NetworkObject>().Despawn();
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestDragServerRpc(Vector3 pos, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            transform.position = pos;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestRotateServerRpc(Vector3 pos, ServerRpcParams serverRpcParams = default)
    {
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {
            var client = NetworkManager.ConnectedClients[clientId];
            transform.LookAt(pos);
        }
    }
}

