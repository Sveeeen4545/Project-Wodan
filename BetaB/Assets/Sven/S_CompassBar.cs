using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class S_CompassBar : MonoBehaviour
{
    public RectTransform compassBarTransform;

    public RectTransform objectiveMarkerTransform;
    public RectTransform northMarkerTransform;
    public RectTransform southMarkerTransform;
    public RectTransform eastMarkerTransform;
    public RectTransform westMarkerTransform;

    public Transform cameraObjectTransform;
    public Transform objectiveObjectTransform;

    private float north, south,east,west;

    //[SerializeField] private float visibilityDistanceThreshold = 1000f;

    private void Update()
    {
        SetMarkerPosition(objectiveMarkerTransform, objectiveObjectTransform.position);
        north = SetMarkerPosition(northMarkerTransform, Vector3.forward * 1000);
        south = SetMarkerPosition(southMarkerTransform, Vector3.back * 1000);
        east = SetMarkerPosition(eastMarkerTransform, Vector3.right * 1000);
        west = SetMarkerPosition(westMarkerTransform, Vector3.left * 1000);
        HideMarkerWithHighestDistance();

    }

    private float SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {


        markerTransform.gameObject.SetActive(true);
        Vector3 direcetionToTarget = worldPosition - cameraObjectTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(direcetionToTarget.x, direcetionToTarget.z), new Vector2(cameraObjectTransform.transform.forward.x, cameraObjectTransform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
        return angle; 


    }

    private void HideMarkerWithHighestDistance()
    {
        float highest = Mathf.Max(Mathf.Abs(north), Mathf.Abs(south), Mathf.Abs(east), Mathf.Abs(west));

        if (highest == north)
        {
            northMarkerTransform.gameObject.SetActive(false);
        }
        if (highest == south)
        {
            southMarkerTransform.gameObject.SetActive(false);
        }
        if (highest == east)
        {
            eastMarkerTransform.gameObject.SetActive(false);
        }
        if (highest == west)
        {
            westMarkerTransform.gameObject.SetActive(false);
        }
    }
}
