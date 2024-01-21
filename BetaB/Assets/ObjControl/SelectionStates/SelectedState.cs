using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class SelectedState : StateMachineBehaviour
{
    private SelectionTracker selectionTracker;
    private SceneObjControl sceneObjControl;

    //public GameObject buttonParent;
    //public RectTransform uiElementprefab;
  
    private GameObject toolbar;
    private InputUI _inputUI;

    private bool requestChange;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        requestChange = false;
        selectionTracker = GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<SelectionTracker>();
        sceneObjControl = selectionTracker.Selection;

        toolbar = CanvasHandeler.instance.toolbar;
        _inputUI = CanvasHandeler.instance.inputUI.GetComponent<InputUI>();


        toolbar.SetActive(true);

        if ((sceneObjControl.hazards.Length > 0 || sceneObjControl.victims.Length > 0))
        {            
            CanvasHandeler.instance.inputUI.gameObject.SetActive(true);
            //_inputUI.isNewVictim(true);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //.newVictim = false;


        if (requestChange)
        {
            animator.SetInteger("SelectionState", 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
             
            if(ClickedOther())
            {
                requestChange = true;
            }

        }
        
        
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CanvasHandeler.instance.toolbar.SetActive(false);
        CanvasHandeler.instance.inputUI.SetActive(false);

    }

    private bool ClickedOther()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return false;
        }

        if (Physics.Raycast(ray, out hit, 500))
        {
            if (hit.collider.GetComponent<SceneObjControl>() == sceneObjControl)
            {
                return false;
            }

        }

        return true;
    }

}
