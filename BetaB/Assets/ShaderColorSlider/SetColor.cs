using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class SetColor : NetworkBehaviour
{

    [Range(1, 5)] public NetworkVariable<int> value;

    [SerializeField] private GameObject colorwheel;

    


    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        colorwheel = CanvasHandeler.instance.colourWheel; 


        colorwheel.SetActive(true);

        colorwheel.GetComponent<Colourwheel>().setColor = this; 


        //GameObject wheel = Instantiate(colorwheel, CanvasHandeler.instance.transform);

        //foreach (Button button in wheel.GetComponentsInChildren<Button>())
        //{
        //    button.onClick.AddListener(() => SetValue(index));
        //    index++;
        //}
    }

    void Update()
    {
        foreach (Transform child in transform)
        {
            switch (value.Value)
            {
                case 0: child.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow); break;
                case 1: child.GetComponent<Renderer>().material.SetColor("_Color", Color.red); break;
                case 2: child.GetComponent<Renderer>().material.SetColor("_Color", Color.white); ; break;
                case 3: child.GetComponent<Renderer>().material.SetColor("_Color", Color.blue); ; break;
                case 4: child.GetComponent<Renderer>().material.SetColor("_Color", Color.black); break;
                case 5: child.GetComponent<Renderer>().material.SetColor("_Color", Color.grey); break;

                default: break;
            }
        }
    }

    public void SetValue(int colorindex)
    {
        value.Value = colorindex;

        //DestroyImmediate(colorwheel);
    }
}
