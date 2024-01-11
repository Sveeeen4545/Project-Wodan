using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;

public class SceneData : NetworkBehaviour
{

    ////////////////////////////////////////////////////////////
    //cLient has info on victim: 
    //Make server request with struct variables
    // Server makes structed victims

    // UPDATE VICTIM COUNTER 
    // RETRIEVE VICTIM LIST ON CLICK 

    /// CLASS VICTIMS THAT CONTAINTS LIST VICTIMS 
    /////////////////////////////////////////////////////////

    [SerializeField] private List<Victim> victimList;

    [SerializeField] private TextMeshProUGUI _victimCounter;


    void Update()
    {
        //if (IsServer && victimList != null) {networkVictimCount.Value = victimList.Count.ToString(); }

        //Debug.Log(victimList.Count);

        if (_victimCounter != null && victimList != null){
            _victimCounter.text = victimList.Count.ToString();

        }
    }

    public void ModifyVictim()
    {
        ModifyVictimServerRPC(60f, "This is victim " + (victimList.Count + 1).ToString(), "Adult", true);
    }

    [ServerRpc(RequireOwnership = false)]
    private void ModifyVictimServerRPC(float priority, string notes, string age, bool hasPulse, ServerRpcParams serverRpcParams = default)
    {
        ModifyVictimClientRPC(priority, notes, age, hasPulse);
        
    }


    [ClientRpc]
    private void ModifyVictimClientRPC(float priority, string notes, string age, bool hasPulse)
    {
        Victim new_victim = gameObject.AddComponent<Victim>();

        new_victim.priority = priority;
        new_victim.notes = notes;
        new_victim.age = age;
        new_victim.hasPulse = hasPulse;

        victimList.Add(new_victim);
    }

}




public class Victim : MonoBehaviour
{
    //public Vector3 Location;
    //[Range 0f, 100f]
    public float priority;
    public string notes;
    public string age;
    public bool hasPulse;
    


    public Victim(float _prio, string _notes, string _age, bool _haspulse)
    {
        priority = _prio;
        notes = _notes;
        age = _age;
        hasPulse = _haspulse;
    }
}


public class Hazzard : MonoBehaviour
{
    public string type ;
    public float prio; 

}

