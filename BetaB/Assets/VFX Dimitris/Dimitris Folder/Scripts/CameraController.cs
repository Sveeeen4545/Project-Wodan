using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] bool zoomEnabeled; 

    [Header("Look Sensitivity")]
    public float sensX = 2f;
    public float sensY = 2f;

    [Header("Clamping")]
    public float minY = -90f;
    public float maxY = 90f;

    [Header("Spectator")]
    public float spectatorMoveSpeed = 5f;


    [Header("Rotation")]
    private float rotX = 0f;
    private float rotY = 0f;

    private Transform _target;
    [SerializeField] private Transform _middlePoint;

    [SerializeField] private float _distanceToTarget =3f;
    private Vector3 _currentRotation;
    private Vector3 _cameraVelocity = Vector3.zero;
    [SerializeField] private float _smoothTime = 0.2f, cameraXrot;

    [SerializeField] private Transform cameraposition;


    private SelectionTracker _selectionTracker; 

    void Start()
    {
        rotX = cameraXrot;
        _selectionTracker = GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<SelectionTracker>();
    }


    private void LateUpdate()
    {
        CameraZoom();
        CameraPosition();
        CameraRotation();
    }

    private void CameraPosition()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0;

        if (Input.GetKey(KeyCode.Space))
            y = 1;
        else if (Input.GetKey(KeyCode.LeftShift))
            y = -1;

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0; 
        right.y = 0;   

        Vector3 dir = (forward.normalized * z + right.normalized * x + transform.up * y).normalized;
        transform.position += dir * spectatorMoveSpeed * Time.deltaTime;
    }

    private void CameraRotation()
    {
        int currentState = _selectionTracker.GetComponent<Animator>().GetInteger("SelectionState");

        float rotInput = Input.GetAxis("Rotate") * sensY;

        SetTarget();

        rotY += rotInput;

        Vector3 nextRotation = new Vector3(rotX, rotY, 0);
        _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _cameraVelocity, _smoothTime);

        transform.localEulerAngles = _currentRotation;

        _distanceToTarget = Vector3.Distance(transform.position, _target.position);


        if (currentState == 1)
        {
            Vector3 desiredPosition = new Vector3(0, 0, 0);

            desiredPosition = _target.position - transform.forward * _distanceToTarget;
            desiredPosition.y = transform.position.y;

            transform.position = desiredPosition; 
        }
    }

    

    private void SetTarget()
    {
        if (_selectionTracker.Selection != null)
        {
            _target = _selectionTracker.Selection.transform;
        }
        else if (_middlePoint != null)
        {
            _target = _middlePoint;
        }
        else
        {
            _target.position = Vector3.zero;
        }

        Vector3 camera = transform.position;
        camera.y = 0; 

        Vector3 target = _target.position;
        target.y = 0;
    }

    private void CameraZoom()
    {
        if (zoomEnabeled)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                GetComponent<Camera>().fieldOfView--;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                GetComponent<Camera>().fieldOfView++;
            }
        } 
    }
}
