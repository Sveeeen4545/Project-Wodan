using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HazardPlacementState : StateMachineBehaviour
{

    private SceneObjControl dropper;
    private SelectionTracker selectionTracker;
    private LayerMask _targetLayer;
    [SerializeField] private NetworkSpawner spawner;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        selectionTracker = GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<SelectionTracker>();
        spawner = selectionTracker.GetComponent<NetworkSpawner>();

        _targetLayer = LayerMask.GetMask("Surface");

        dropper = Instantiate(selectionTracker.dropperPrefab).GetComponent<SceneObjControl>();

        selectionTracker.SetSelection(dropper);

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 500, _targetLayer) && selectionTracker.Selection != null)
        {
            selectionTracker.Selection.transform.position = hit.point;

            if (Input.GetMouseButtonDown(1) && dropper != null)
            {
                Destroy(dropper.gameObject);
                animator.SetInteger("SelectionState", 0);
                return;
            }


            if (Input.GetMouseButtonDown(0))
            {
                //Destroy(dropper.gameObject);
                if (Physics.Raycast(ray, out hit, 500))
                {

                    if (hit.collider.GetComponent<SceneObjControl>() != null)
                    {
                    Debug.Log("add Hazard to this object");
                    CanvasHandeler.instance.sceneData.AddHazard(
                        hit.collider.gameObject.GetComponent<NetworkObject>().NetworkObjectId,
                        60f,
                        Hazard.HazardTypes.Eletric
                        );
                    }
                    else
                    {
                        Debug.Log("spawn new object and add Hazard to that object");
                        spawner.GetComponent<NetworkSpawner>().RequestSpawnServerRpc(hit.point, Quaternion.identity, NetworkSpawner.SpawnTypes.Hazard);
                    }
                }
                Destroy(selectionTracker.Selection.gameObject);
                selectionTracker.SetState(0);
            }
        }
    }
}
