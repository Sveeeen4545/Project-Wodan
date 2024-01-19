using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_CompassBar : MonoBehaviour
{
    public RectTransform compassBarTransform;

    public RectTransform objectiveMarkerTransform;
    public RectTransform northMarkerTransform;
    public RectTransform southMarkerTransform;

    public Transform cameraObjectTransform;
    public Transform objectiveObjectTransform;

    private void Update()
    {
        SetMarkerPosition(objectiveMarkerTransform, objectiveObjectTransform.position);
        SetMarkerPosition(northMarkerTransform, Vector3.forward * 1000);
        SetMarkerPosition(southMarkerTransform, Vector3.back * 1000);
    }

    private void SetMarkerPosition(RectTransform markerTransform, Vector3 worldPosition)
    {
        Vector3 direcetionToTarget = worldPosition - cameraObjectTransform.position;
        float angle = Vector2.SignedAngle(new Vector2(direcetionToTarget.x, direcetionToTarget.z), new Vector2(cameraObjectTransform.transform.forward.x, cameraObjectTransform.transform.forward.z));
        float compassPositionX = Mathf.Clamp(2 * angle / Camera.main.fieldOfView, -1, 1);
        markerTransform.anchoredPosition = new Vector2(compassBarTransform.rect.width / 2 * compassPositionX, 0);
    }
}
