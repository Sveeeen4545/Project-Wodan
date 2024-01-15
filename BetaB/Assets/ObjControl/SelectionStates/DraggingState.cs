using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DraggingState : StateMachineBehaviour
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

        if (animator.GetInteger("SelectionState") != 2) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetInteger("SelectionState", 1);
            return; 
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500, targetLayer))
        {
            sceneObjControl.RequestDrag(hit.point);
        }
    }
}
