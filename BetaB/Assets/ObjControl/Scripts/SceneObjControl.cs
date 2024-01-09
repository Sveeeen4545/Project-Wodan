using Unity.Burst.CompilerServices;
using Unity.Netcode;
using UnityEngine;

public class SceneObjControl : NetworkBehaviour
{
    private LayerMask targetLayer;
    private LayerMask removalLayer;

    private Quaternion _targetQuaternion;

    private bool _isplaced = false;
    private bool _markedForRemoval = false;


    private void Start()
    {
        targetLayer = LayerMask.GetMask("Surface");
        removalLayer = LayerMask.GetMask("Delete");


    }

    //private void Update()
    //{
    //    if (!IsOwner)
    //    {
    //        _isplaced = true;
    //    }

    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        RequestRemovalServerRPC();
    //    }


    //    //if (!_isplaced)
    //    //{
    //    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    //    RaycastHit hit;
    //    //    if (Physics.Raycast(ray, out hit, 500, targetLayer))
    //    //    {
    //    //        RequestRotateServerRpc(hit.point);
    //    //    }
    //    //    if (Input.GetMouseButtonDown(0))
    //    //    {
    //    //        _isplaced = true;
    //    //    }
    //    //}
    //}

    //private void OnMouseDown()
    //{
    //    _isplaced = true;
    //}

    //private void OnMouseUp()
    //{
    //    if (_markedForRemoval)
    //    {
    //        RequestRemovalServerRPC();
    //        return;
    //    }
    //}

    //private void OnMouseDrag()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

        
    //    if (Physics.Raycast(ray, out hit, 500, targetLayer))
    //    {
    //        RequestDragServerRpc(hit.point);
    //    }
    //}

    public void RequestRemoval()
    {
        RequestRemovalServerRPC(); 
    }

    public void RequestDrag(Vector3 pos)
    {
        RequestDragServerRpc(pos);
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
        //var clientId = serverRpcParams.Receive.SenderClientId;
        //if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        //{
        //    var client = NetworkManager.ConnectedClients[clientId];
        //    transform.LookAt(pos);
        //}

    }
}

