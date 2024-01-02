using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class OwnerControl : NetworkBehaviour
{

    private bool IsSelected;  

    public override void OnNetworkSpawn()
    {
        if (!IsOwner) Destroy(this); 

    }


    void Update()
    {
       
    }

    void OnSelect()
    {
        if (IsSelected)
        {
            Debug.Log("Already selected");
            return; 
        }

        Debug.Log("Newly Selected");

    }

}
