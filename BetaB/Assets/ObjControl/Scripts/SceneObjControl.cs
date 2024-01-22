using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SceneObjControl : NetworkBehaviour
{
    
    SelectionTracker selectionTracker;

    private Hazard hazard;

    private bool _hazardAttached = false;


    [SerializeField]private List<GameObject> componentIcons = new List<GameObject>();

    private Vector3 screenPos;
    private int index;
    private GameObject icon;
    private Gradient gradient;
    public Victim[] victims;
    public Hazard[] hazards;


    private void Start()
    {
        selectionTracker = GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<SelectionTracker>();
        gradient = CanvasHandeler.instance.prioGradient;

    }

    private void Update()
    {
        victims = gameObject.GetComponents<Victim>();

        hazards = gameObject.GetComponents<Hazard>();



        AttachHazard();
        DisplayComponents();
        
    }

    private void DisplayComponents()
    {
        if (componentIcons.Count > 0) 
        {
            foreach (var item in componentIcons)
            {
                Destroy(item);
            }
        }
        
         index = 0; 
        
         screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if ( victims != null )
        {

            foreach (Victim victim in victims)
            {
                index++; 
                CreateIcon(CanvasHandeler.instance.victimSprite, victim.priority);
            }
        }
        
        if ( hazards != null )
        {
            foreach (Hazard hazard in hazards)
            {
                index++;
                CreateIcon(CanvasHandeler.instance.hazardSprite, hazard.prio);

            }
        }
    }

    private void CreateIcon(Sprite image, float prio)
    {
        

        icon = Instantiate(CanvasHandeler.instance.VictimIconPrefab, CanvasHandeler.instance.transform);
        componentIcons.Add(icon);
        Vector3 pos = screenPos;

        

        icon.GetComponent<Image>().sprite = image;

        float normalizedValue = prio / 100f;

        icon.GetComponent<Image>().color = gradient.Evaluate(normalizedValue);

        if (!CanvasHandeler.instance.toolbar.activeInHierarchy)
        {
            pos.y = pos.y + 100;
            pos.x = pos.x + (index * CanvasHandeler.instance.IconOffset) - 140;
            icon.transform.position = pos;
        }
        else if(GameObject.FindWithTag("SelectionTracker").GetComponent<SelectionTracker>().Selection == this)
        {
            pos.y = pos.y + (index * CanvasHandeler.instance.IconOffset) + 280 ;
            pos.x = pos.x- 240;
            icon.transform.position = pos;
            icon.AddComponent<Button>();
        }
        else
        {
            Destroy(icon);
        }
    }

    public void tabButton()
    {
        CanvasHandeler.instance.GetComponent<InputUI>().OpenInputUI(GetComponent<NetworkObject>().NetworkObjectId); 
    }

    private void AttachHazard()
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
                        if (selectionTracker.HazardEffects[1] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[1], this.transform);
                        }
                        break;
                    case Hazard.HazardTypes.Eletric:
                        if (selectionTracker.HazardEffects[2] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[2], this.transform);
                        }
                        break;
                    case Hazard.HazardTypes.Water:
                        if (selectionTracker.HazardEffects[6] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[6], this.transform);
                        }
                        break;
                    case Hazard.HazardTypes.Smoke:
                        if (selectionTracker.HazardEffects[3] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[3], this.transform);
                        }
                        break;
                    case Hazard.HazardTypes.Chemical:
                        if (selectionTracker.HazardEffects[4] != null)
                        {
                            Instantiate(selectionTracker.HazardEffects[4], this.transform);
                        }
                        break;
                    default: Instantiate(selectionTracker.HazardEffects[0], this.transform); break;
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

    private void OnDestroy()
    {


        if (componentIcons.Count > 0)
        {
            foreach (var item in componentIcons)
            {
                Destroy(item);
            }
        }


        if (victims != null)
        {
            foreach (Victim victim in victims)
            {
                CanvasHandeler.instance.sceneData.victimList.Remove(victim);
            }
        }

        if (hazards != null)
        {
            foreach (Hazard hazard in hazards)
            {
                CanvasHandeler.instance.sceneData.hazardList.Remove(hazard);
            }
        }

        
        
    }

    
}

