using Unity.Netcode;
using UnityEngine;

public class SceneObjControl : NetworkBehaviour
{
    private LayerMask targetLayer;
    private Quaternion _targetQuaternion;

    private bool _isplaced = false;


    private void Start()
    {
        targetLayer = LayerMask.GetMask("Surface");
    }

    private void Update()
    {
        if (!IsOwner)
        {
            _isplaced = true;
        }


        if (!_isplaced)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, targetLayer))
            {
                RequestRotateServerRpc(hit.point);
            }
            if (Input.GetMouseButtonDown(0))
            {
                _isplaced = true;
            }
        }
    }

    private void OnMouseDown()
    {
        _isplaced = true;
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, targetLayer))
        {
            RequestDragServerRpc(hit.point);
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

