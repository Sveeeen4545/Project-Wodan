using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CreateVictimUI : MonoBehaviour
{
    [SerializeField] GameObject UIElement;
    [SerializeField] GameObject content;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateVictimUIButton()
    {
        Instantiate(UIElement, content.transform);
    }



}
