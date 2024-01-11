using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasHandeler : MonoBehaviour
{
    public static CanvasHandeler instance;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public GameObject toolbar;
}
