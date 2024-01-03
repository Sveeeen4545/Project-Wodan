
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private GameObject m_prefab;
    
    private NetworkSpawner _networkSpawner;
    private GameObject _selected;
    private LayerMask _targetLayer;

    public void Awake()
    {
        _targetLayer = LayerMask.GetMask("Surface");
        _networkSpawner = GetComponent<NetworkSpawner>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _selected = Instantiate(m_prefab);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, _targetLayer))
        {
            _selected.transform.position = hit.point;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_selected != null)
        {
            _networkSpawner.RequestSpawnServerRpc( _selected.transform.position);
            Destroy(_selected);
            _selected = null;
        }
    }
}