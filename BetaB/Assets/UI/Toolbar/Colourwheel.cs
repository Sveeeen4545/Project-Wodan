using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colourwheel : MonoBehaviour
{

    public int colorIndex;
    public SetColor setColor;



    //private bool wheelfound = false;

    // void Update()
    //{
    //    if (!wheelfound)
    //    {
    //        setColor = GameObject.FindWithTag("SelectionTracker").GetComponent<SelectionTracker>().Selection.transform.Find("Coloured_parts").GetComponent<SetColor>();

    //        wheelfound = true;
    //    }
    //}

    public void SetColourIndex(int index)
    {
        setColor.SetValue(index);
        gameObject.SetActive(false);
    }
}
