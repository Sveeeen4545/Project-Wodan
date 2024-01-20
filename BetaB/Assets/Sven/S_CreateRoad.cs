using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CreateRoad : MonoBehaviour
{
    [SerializeField] private GameObject S_SingleLane;
    [SerializeField] private GameObject S_DoubleLane;
    [SerializeField] private GameObject S_CurvedRoad;
    [SerializeField] private GameObject S_Intersection;

    [SerializeField] private GameObject RoadPanel;

    public void SpawnSingleLane()
    {
        Instantiate(S_SingleLane);
        RoadPanel.SetActive(false);
    }
    public void SpawnDoubleLane()
    {
        Instantiate (S_DoubleLane);
        RoadPanel.SetActive(false);

    }
    public void SpawnCurvedRoad()
    {
        Instantiate(S_CurvedRoad);
        RoadPanel.SetActive(false);
    }
    public void SpawnIntersection()
    {
        Instantiate(S_Intersection);
        RoadPanel.SetActive(false);
    }
}
