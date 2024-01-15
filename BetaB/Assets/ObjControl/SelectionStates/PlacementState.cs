using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementState : StateMachineBehaviour
{
    private SelectionTracker selectionTracker;
    private SceneObjControl sceneObjControl;
    private LayerMask _targetLayer;
    private bool _rotating;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        selectionTracker = GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<SelectionTracker>();
        sceneObjControl = Instantiate(selectionTracker.Selection);
        _targetLayer = LayerMask.GetMask("Surface");
        _rotating = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500, _targetLayer))
        {
            if (Input.GetMouseButtonDown(1) && sceneObjControl != null)
            {
                _rotating = false;
                Destroy(sceneObjControl.gameObject);
                animator.SetInteger("SelectionState", 0);
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (!_rotating)
                {
                    _rotating = true;
                }
                else {
                    Quaternion rot = sceneObjControl.transform.rotation;
                    Vector3 pos = sceneObjControl.transform.position;
                    Destroy(sceneObjControl.gameObject);
                    selectionTracker.RequestSpawnServerRpc(pos, rot);

                    animator.SetInteger("SelectionState", 0);
                    return;
                }
            }

            if (sceneObjControl != null && !_rotating)
            {
                sceneObjControl.transform.position = hit.point;
            }
            else if (_rotating && sceneObjControl != null)
            {
                sceneObjControl.transform.LookAt(hit.point);
            }
        }
    }


}
