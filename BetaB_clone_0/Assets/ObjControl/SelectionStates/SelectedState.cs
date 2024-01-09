using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using static UnityEngine.GraphicsBuffer;

public class SelectedState : StateMachineBehaviour
{

    private SelectionTracker selectionTracker;
    private SceneObjControl sceneObjControl;

    public GameObject canvasPrefab;
    public RectTransform uiElementprefab;
  
    private GameObject toolbar;

    private bool requestChange; 



    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        requestChange = false;
        selectionTracker = GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<SelectionTracker>();

        sceneObjControl = selectionTracker.Selection;

        Debug.Log("Show UI options");


        toolbar = CanvasHandeler.instance.toolbar;
        toolbar.SetActive(true);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(sceneObjControl.transform.position);

        toolbar.transform.position = screenPos;

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // CHECK IF THE CLICK IS ON THE DRAG OPTION 

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500))
            {
                if (requestChange) 
                {
                    animator.SetInteger("SelectionState", 0);

                }

                if (hit.collider.GetComponent<SceneObjControl>() == null /*|| animator.GetInteger("SelectionState") != 2*/)
                {
                    requestChange = true;
                }
            }

        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        CanvasHandeler.instance.toolbar.SetActive(false);

    }

    
}
