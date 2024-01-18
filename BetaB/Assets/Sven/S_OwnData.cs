using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_OwnData : MonoBehaviour
{
    public Victim victim;
    public SceneData sceneData;

    public S_VictimDataGrab objectVicitm;

    private List<SceneData> victimList;

    public TextMeshProUGUI prioText;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI hasPulseText;
    public TextMeshProUGUI notesText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SceneData victim in victimList)
        {
            if (victim != null)
            {
                Instantiate(victim);
            }
        }






        prioText.text = $"Prioroty: {victim.priority}";
        ageText.text = $"Age: {victim.age}";
        hasPulseText.text = $"Has Pulse: {victim.hasPulse}";
        notesText.text = $"Notes: {victim.notes}";
    }
}
