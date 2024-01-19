using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Collections;
using JetBrains.Annotations;
using static Hazard;

public class SceneData : NetworkBehaviour
{
    public List<Victim> victimList;
    public List<Hazard> hazardList;

    [SerializeField] private TextMeshProUGUI _victimCounter;
    [SerializeField] private TextMeshProUGUI _hazardCounter;

    void Update()
    {
        if (_victimCounter != null && victimList != null){
            _victimCounter.text = victimList.Count.ToString();
        }

        if (_hazardCounter != null && hazardList != null ) 
        { 
            _hazardCounter.text = hazardList.Count.ToString();
        }
    }

    public void AddVictim(ulong objectid, float prio, string notes, string age, bool hasPulse)
    {
        AddVictimServerRPC(objectid, prio, notes, age, hasPulse);
    }

    [ServerRpc(RequireOwnership = false)]
    private void AddVictimServerRPC(ulong objectid, float priority, string notes, string age, bool hasPulse, ServerRpcParams serverRpcParams = default)
    {
        AddVictimClientRPC(objectid, priority, notes, age, hasPulse);
    }


    [ClientRpc]
    private void AddVictimClientRPC(ulong objectid, float priority, string notes, string age, bool hasPulse)
    {
        //GameObject derp = GetNetworkObject(location.NetworkObjectId).gameObject;


        GameObject Locaton = GetNetworkObject(objectid).gameObject;

        Victim new_victim = Locaton.AddComponent<Victim>();

        new_victim.priority = priority;
        new_victim.notes = notes;
        new_victim.age = age;
        new_victim.hasPulse = hasPulse;

        victimList.Add(new_victim);
    }


    public void AddHazard(ulong objectid, float prio, HazardTypes type)
    {
        AddHazardServerRPC(objectid, prio,type);
    }


    [ServerRpc(RequireOwnership = false)]
    private void AddHazardServerRPC(ulong objectid, float prio, HazardTypes type, ServerRpcParams serverRpcParams = default)
    {
        AddHazardClientRPC(objectid, prio, type);
    }

    [ClientRpc]
    private void AddHazardClientRPC(ulong objectid, float prio, HazardTypes type)
    {
        GameObject Locaton = GetNetworkObject(objectid).gameObject;

        Hazard new_Hazard = Locaton.AddComponent<Hazard>();
        new_Hazard.prio = prio;
        new_Hazard.type = type;

        hazardList.Add(new_Hazard);
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


public class Hazard : MonoBehaviour
{
    public enum HazardTypes
    {
        Fire,
        Eletric,
        Water,
        Chemical, 
        Smoke
    }

    public HazardTypes type ;
    public float prio;

    public Hazard(HazardTypes type, float prio)
    {
        this.type = type;
        this.prio = prio;
    }
}

