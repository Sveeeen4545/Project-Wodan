using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour , IPointerDownHandler, IDragHandler
{
    [SerializeField] GameObject m_prefab;
    private GameObject _selected;
    private LayerMask targetLayer;



    private Vector3 mousePos;

    public void Awake()
    {
        targetLayer = LayerMask.GetMask("Surface");

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("clicked");
        mousePos = Input.mousePosition - GetMousePos();
        _selected = Instantiate(m_prefab);
        _selected.GetComponent<NetworkObject>().Spawn();
    }

    public void OnDrag(PointerEventData eventData)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, targetLayer))
        {
            _selected.transform.position = hit.point;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        //InstantiateServerRpc(m_prefab, GetMousePos());
    }

    [ServerRpc]
    private void InstantiateServerRpc(GameObject item, Vector3 pos)
    {
        InstantiateClientRpc(item, pos);
    }

    [ClientRpc]
    private void InstantiateClientRpc(GameObject item, Vector3 pos)
    {
        
        Instantiate(item, pos, Quaternion.identity);


    }


        private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}
