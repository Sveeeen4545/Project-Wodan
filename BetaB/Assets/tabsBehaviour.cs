using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class tabsBehaviour : NetworkBehaviour
{

    [SerializeField] private GameObject _toolsTab, _vehiclesTab, _environmentTab, _toolsItems, _environmentItems, _vehicleItems;

    private bool initialized = false;

    

    // Update is called once per frame
    void Update()
    {
        if (initialized) {  return; }

        if(IsServer)
        {
            _toolsTab.SetActive(false);
            _toolsItems.SetActive(false);
            _vehicleItems.SetActive(true);
            _environmentItems.SetActive(false);

            initialized = true;
        }
        if (IsClient && !IsServer)
        {
            _vehiclesTab.SetActive(false);
            _environmentTab.SetActive(false);
            _toolsItems.SetActive(true);
            _vehicleItems.SetActive(false);
            _environmentItems.SetActive(false);
            initialized = true;

        }
    }
}
