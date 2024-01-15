using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SceneObjControl : NetworkBehaviour
{
    
    SelectionTracker selectionTracker;

    private Hazard hazard;

    private bool _hazardAttached = false;

    private void Start()
    {
        selectionTracker = GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<SelectionTracker>();
        
    }

    private void Update()
    {
        if (!_hazardAttached)
        {
            hazard = GetComponent<Hazard>();
            if (hazard != null)
            {
                _hazardAttached = true;
                switch (hazard.type)
                {
                    case Hazard.HazardTypes.Fire:
                        if (selectionTracker.HazardEffects[0] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[0],this.transform);
                        }
                        break;
                    case Hazard.HazardTypes.Eletric:
                        if (selectionTracker.HazardEffects[1] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[1],this.transform);
                        }
                        break;
                    case Hazard.HazardTypes.Water:
                        if (selectionTracker.HazardEffects[2] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[2]);
                        }
                        break;
                    case Hazard.HazardTypes.Smoke:
                        if (selectionTracker.HazardEffects[3] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[3]);
                        }
                        break;
                    case Hazard.HazardTypes.Chemical:
                        if (selectionTracker.HazardEffects[4] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[4]);
                        }
                        break;
                    default: Debug.Log("ERROR unkown type");  break;     
                }


            }

        }
    }



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

