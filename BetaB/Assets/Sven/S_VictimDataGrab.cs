using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_VictimDataGrab : MonoBehaviour
{
    [SerializeField] private GameObject S_VictimPrefab;
    [SerializeField] private GameObject contentTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateVictimObject()
    {
        Instantiate(S_VictimPrefab, contentTarget.transform);
    }

}
