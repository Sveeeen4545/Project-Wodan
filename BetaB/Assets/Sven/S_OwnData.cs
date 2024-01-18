using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class S_OwnData : MonoBehaviour
{
    Victim victim;

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
        prioText.text = $"Prioroty: {victim.priority}";
        ageText.text = $"Age: {victim.age}";
        hasPulseText.text = $"Has Pulse: {victim.hasPulse}";
        notesText.text = $"Notes: {victim.notes}";
    }
}
