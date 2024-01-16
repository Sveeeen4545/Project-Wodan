using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictimComponent : MonoBehaviour
{
    [SerializeField] private SceneData data;
    private List<Hazard> hazardList;
    private List<Victim> victimList;

    private void Start()
    {
        hazardList = data.hazardList;
        victimList = data.victimList;
    }

    public void Testing()
    {
        
    }

}
