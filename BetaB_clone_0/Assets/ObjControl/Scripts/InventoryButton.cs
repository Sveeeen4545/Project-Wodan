
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI.Table;

public class InventoryButton : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private GameObject m_prefab;
    
    private NetworkSpawner _networkSpawner;
    private GameObject _selected;
    private LayerMask _targetLayer;
    private bool _rotating = false; 

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
        if (Physics.Raycast(ray, out hit, 500, _targetLayer) && _selected != null)
        {
            _selected.transform.position = hit.point;
        }
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_selected != null)
        {
            if (!_rotating)
            {
                _rotating = true;
                return;
            }
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1) && _selected != null)
        {
            _rotating = false;
            Destroy(_selected);
            _selected = null;
            return;
        }

        if (_rotating) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500, _targetLayer))
            {
                _selected.transform.LookAt(hit.point);
            }
            if (Input.GetMouseButtonDown(0))
            {
                _rotating = false;
                _networkSpawner.RequestSpawnServerRpc(_selected.transform.position, _selected.transform.rotation, NetworkSpawner.SpawnTypes.Empy);
                Destroy(_selected);
                _selected = null;
            }
            
        }
    }
}