using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationState : StateMachineBehaviour
{
    private SelectionTracker selectionTracker;
    private SceneObjControl sceneObjControl;
    private LayerMask targetLayer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        selectionTracker = GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<SelectionTracker>();
        sceneObjControl = selectionTracker.Selection;
        targetLayer = LayerMask.GetMask("Surface");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("SelectionState", 1);
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500, targetLayer))
        {
            sceneObjControl.RequestRotate(hit.point);
        }
    }
}
