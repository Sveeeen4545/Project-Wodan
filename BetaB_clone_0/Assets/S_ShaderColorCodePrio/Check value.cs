using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkvalue : MonoBehaviour
{
    [Range(0, 100)]
    public float CheckValues;

    public Color Decent=Color.green;
    public Color Mid=Color.yellow;
    public Color Grave=Color.red;

    Color materialColor;


    void Start()
    {
        materialColor = GetComponent<Renderer>().material.GetColor("_Color");

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckValues < 25)
        {
            GetComponent<Renderer>().material.SetColor("_Color",Decent);

        }

        if (CheckValues > 25 && CheckValues < 50)
        {
            GetComponent<Renderer>().material.SetColor("_Color",Mid);

        }

        if (CheckValues > 50)
        {
            GetComponent<Renderer>().material.SetColor("_Color", Grave);

        }
    }
}
