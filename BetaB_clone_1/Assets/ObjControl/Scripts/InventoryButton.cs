
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private GameObject networkPrefab;
    
    private NetworkSpawner m_networkSpawner;
    private GameObject _selected;
    private LayerMask targetLayer;

    public void Awake()
    {
        targetLayer = LayerMask.GetMask("Surface");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("create dropper prefab");
        _selected = Instantiate(m_prefab);
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
        if (_selected != null)
        {
            Debug.Log("create network obj");


            GetComponent<NetworkSpawner>().RequestSpawnServerRpc( _selected.transform.position);
            
            Destroy(_selected);
            _selected = null;
        }
            

    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }
}