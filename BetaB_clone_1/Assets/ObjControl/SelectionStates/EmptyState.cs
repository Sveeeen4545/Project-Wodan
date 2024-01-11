using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class EmptyState : StateMachineBehaviour
{
    private SelectionTracker selectionTracker;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        selectionTracker = GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<SelectionTracker>();
        selectionTracker.SetSelection(null);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                if(hit.collider.GetComponent<SceneObjControl>() != null)
                {
                    selectionTracker.SetSelection(hit.collider.GetComponent<SceneObjControl>());
                    animator.SetInteger("SelectionState", 1);
                }
            }
        }
    }
}
