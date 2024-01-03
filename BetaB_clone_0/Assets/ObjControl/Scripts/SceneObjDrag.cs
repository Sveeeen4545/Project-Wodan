using Unity.Netcode;
using UnityEngine;

public class SceneObjDrag : NetworkBehaviour
{
    private Vector3 mousePos;
    private LayerMask targetLayer;

    private void Start()
    {
        targetLayer = LayerMask.GetMask("Surface");
    }

    private Vector3 GetMousePos() 
    {
        return Camera.main.WorldToScreenPoint(transform.position); 
    }

    private void OnMouseDown()
    {
        mousePos = Input.mousePosition - GetMousePos();
        //my_client.Value =  NetworkManager.Singleton.LocalClientId;
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, targetLayer))
        {
            RequestDragServerRpc(hit.point);

            //transform.position = hit.point;
        }

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

    [ClientRpc]
    private void RequestDragClientRpc(Vector3 pos)
    {
        
        { ExecuteDrag(pos); }
    }

    private void ExecuteDrag(Vector3 pos)
    {
        transform.position = pos;
    }
}
