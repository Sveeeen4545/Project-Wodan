using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InputUIHazard : NetworkBehaviour
{


    private float _priority;
    private Hazard.HazardTypes _type; 

    private ulong _objectID;
    private int _index;
    



    public void SetType(int Age)
    {

        switch (Age)
        {
            case 0: _type = Hazard.HazardTypes.Fire; break;
            case 1: _type = Hazard.HazardTypes.Eletric; break;
            case 2: _type = Hazard.HazardTypes.Smoke; break;
            case 3: _type = Hazard.HazardTypes.Chemical; break;
            default: Debug.Log("ERROR: uknown HazardType") ; break;
        }
    }

    public void SetPrio(float prio)
    {
        _priority = prio;
    }


    public void ConfirmInput()
    {


        if (GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<Animator>().GetBool("newHazard"))
        {
            Debug.Log("add victim to this object");
            CanvasHandeler.instance.sceneData.AddHazard(_objectID, _priority, _type);
            GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<Animator>().SetBool("newHazard", false);

        }
        else
        {
            Debug.Log("modify selected");
            //CanvasHandeler.instance.sceneData.ModifyHazard(_objectID, _index, _priority, _type);

        }
        CanvasHandeler.instance.inputUIHazard.SetActive(false);
    }

    public void OpenInputUI(ulong objectID, int index = 0)
    {
        if (index > 0)
        {
            _index = index;
            SceneObjControl Location = GetNetworkObject(objectID).gameObject.GetComponent<SceneObjControl>();

            Hazard selectedHazard = Location.hazards[index];

            _objectID = objectID;
            _type = selectedHazard.type;
            _priority = selectedHazard.prio;
 
            return;
        }

        _objectID = objectID;
        _type = default;
        _priority = 0;
    }


}
