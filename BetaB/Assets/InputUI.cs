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

    public void SetPulse()
    {
        _pulse = !_pulse;
    }

    public void SetPrio(float Prio)
    {
        _priority = Prio;
    }

    public void ConfirmInput()
    {
        Debug.Log("add victim to this object");

        CanvasHandeler.instance.sceneData.AddVictim(_objectID, _priority, _notes, _age,_pulse);
        CanvasHandeler.instance.inputUI.SetActive(false);
    }

    public void OpenInputUI(ulong objectID)
    {
        _objectID = objectID;
        _age= "Unknown";
        _priority = 0;
        _pulse = false;
        _notes = ""; 
    }


}
