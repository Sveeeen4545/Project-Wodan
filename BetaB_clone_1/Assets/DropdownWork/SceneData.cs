using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;
using JetBrains.Annotations;

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

    //"This is victim " + (victimList.Count + 1).ToString()
    void Update()
    {
        //if (IsServer && victimList != null) {networkVictimCount.Value = victimList.Count.ToString(); }
        //Debug.Log(victimList.Count);

        if (_victimCounter != null && victimList != null){
            _victimCounter.text = victimList.Count.ToString();
        }
    }

    public void testVictimAdd()
    {
        //NetworkObject networkObject = new NetworkObject();
        //NetworkObjectReference networkObjectReference = networkObject;

        AddVictim(

            GetComponent<NetworkObject>().NetworkObjectId,
            60f,
            "I am patient 0",
            "Adult",
            true
            );
    }

    public void AddVictim(ulong location, float prio, string notes, string age, bool hasPulse)
    {
        AddVictimServerRPC(location, prio, notes, age, hasPulse);
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddVictimServerRPC(ulong location, float priority, string notes, string age, bool hasPulse, ServerRpcParams serverRpcParams = default)
    {
        AddVictimClientRPC(location, priority, notes, age, hasPulse);
    }


    [ClientRpc]
    private void AddVictimClientRPC(ulong location, float priority, string notes, string age, bool hasPulse)
    {
        //GameObject derp = GetNetworkObject(location.NetworkObjectId).gameObject;


        GameObject VictimLocaton = GetNetworkObject(location).gameObject;

        Victim new_victim = VictimLocaton.AddComponent<Victim>();

        new_victim.priority = priority;
        new_victim.notes = notes;
        new_victim.age = age;
        new_victim.hasPulse = hasPulse;

        victimList.Add(new_victim);
    }

}




public class Victim : MonoBehaviour
{
    
    public float priority;
    public string notes;
    public string age;
    public bool hasPulse;
    


    public Victim(float priority, string notes, string age, bool haspulse)
    {
        this.priority = priority;
        this.notes = notes;
        this.age = age;
        this.hasPulse = haspulse;
    }
}


public class Hazzard : MonoBehaviour
{
    public string type ;
    public float prio;


    public Hazzard(string type, float prio)
    {
        this.type = type;
        this.prio = prio;
    }
}

