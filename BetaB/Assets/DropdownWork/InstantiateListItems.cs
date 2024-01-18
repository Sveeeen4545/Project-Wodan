using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateListItems : MonoBehaviour
{
    [SerializeField] private GameObject S_VictimPrefab;
    [SerializeField] private GameObject contentTarget;
    [SerializeField] private SceneData scenedata;
    [SerializeField] private bool isVictimList;
    private bool needsOpening = false; 

    void Start()
    {
        //scenedata = CanvasHandeler.instance.sceneData; 
    }

    void Update()
    {

        if (contentTarget.activeInHierarchy && needsOpening)
        {
            OpenContent();

            needsOpening = false;
        }
        if (!contentTarget.activeInHierarchy && !needsOpening)
        {
            needsOpening = true;
        }


    }


    


    
    private void OpenContent()
    {


        foreach (Transform child in contentTarget.transform)
        {
            Destroy(child.gameObject);
        }

        if (isVictimList)
        {
            foreach (Victim victim in scenedata.victimList)
            {
                if (contentTarget != null)
                {
                    GameObject obj = Instantiate(S_VictimPrefab, contentTarget.transform);

                    S_OwnData ownData = obj.GetComponent<S_OwnData>();

                    if (ownData != null)
                    {
                        ownData.ageText.text = "age: " + victim.age;
                        ownData.notesText.text ="notes: " + victim.notes;
                        if (victim.hasPulse)
                        {
                            ownData.hasPulseText.text = "has Pulse";
                        }
                        else
                        {
                            ownData.hasPulseText.text = "has no Pulse";
                        }
                    }
                }
            }
        }
        else if(!isVictimList)
        {
            foreach (Hazard hazard in scenedata.hazardList)
            {
                if (contentTarget != null)
                {
                    GameObject obj = Instantiate(S_VictimPrefab, contentTarget.transform);

                    S_OwnData ownData = obj.GetComponent<S_OwnData>();

                    if (ownData != null)
                    {
                        ownData.ageText.text = "type: " + hazard.type.ToString();
                        ownData.notesText.text = hazard.prio.ToString();
                        
                        ownData.hasPulseText.text = "";
                    }
                }
            }
        }
    }
}

