using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InputUI : NetworkBehaviour
{
    
    
    private float _priority;
    private string _notes;
    private bool _pulse;
    private string _age;
    private ulong _objectID;
    private int _index;
    private Victim _victim;

    public void isNewVictim(bool isnew)
    {
        //_newVictim = isnew;
    }



    public void SetAge(int Age)
    {

        switch (Age)
        {
            case 0: _age = "Child";break;
            case 1: _age = "Adult";break;
            case 2: _age = "Elderly";break;
            default: _age = "Unknown";break;
        }
    }

    public void SetNotes(string Notes)
    {
        _notes = Notes;
    }

    public void SetPulse(bool pulse)
    {
        _pulse = pulse;
    }

    public void SetPrio(float Prio)
    {
        _priority = Prio;
    }

    public void ConfirmInput()
    {

        
        if (GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<Animator>().GetBool("newVictim"))
        {
            Debug.Log("add victim to this object");
            CanvasHandeler.instance.sceneData.AddVictim(_objectID, _priority, _notes, _age,_pulse);
            GameObject.FindGameObjectWithTag("SelectionTracker").GetComponent<Animator>().SetBool("newVictim", false);

        }
        else
        {
            Debug.Log("modify selected");
            CanvasHandeler.instance.sceneData.ModifyVictim(_objectID, _index, _priority, _notes, _age, _pulse);

        }
        CanvasHandeler.instance.inputUI.SetActive(false);
    }

    public void OpenInputUI(ulong objectID, int index = 0)
    {
        if (index > 0)
        {
            _index = index;
            SceneObjControl Location = GetNetworkObject(objectID).gameObject.GetComponent<SceneObjControl>();

            Victim selectedVictim = Location.victims[index];

            _objectID = objectID;
            _age = selectedVictim.age;
            _priority = selectedVictim.priority;
            _pulse = selectedVictim.hasPulse;
            _notes = selectedVictim.notes;

            return;
        }


        _objectID = objectID;
        _age= "Unknown";
        _priority = 0;
        _pulse = false;
        _notes = ""; 

        
    }


}
