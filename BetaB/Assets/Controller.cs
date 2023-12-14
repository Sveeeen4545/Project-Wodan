using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;

public class Controller : NetworkBehaviour
{

    

    Vector3 mousePos;
    

    private Vector3 GetMousePos() 
    { 
        return Camera.main.WorldToScreenPoint(transform.position); 
    }

    [SerializeField] private float speed= 5f; 
    private CharacterController cc;
    // Start is called before the first frame update

    private void OnMouseDown()
    {
        mousePos = Input.mousePosition - GetMousePos();
        GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.Singleton.LocalClientId);


    }

    private void OnMouseDrag()
    {
        Vector3 desiredPos;
        desiredPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePos);
        //desiredPos.y = 0;
        transform.position = desiredPos;
    }


}
