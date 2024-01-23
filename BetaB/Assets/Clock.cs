using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;

public class Clock : NetworkBehaviour
{

    [SerializeField]private TextMeshProUGUI _clocktext;


    private NetworkVariable<float> gameTime = new NetworkVariable<float>(0f) ;


    private void Start()
    {
        _clocktext = GetComponent<TextMeshProUGUI>();
        gameTime.Value = 0f;
        _clocktext.text = "Not started";
    }

    void Update()
    {

        if(IsServer)
        {
            gameTime.Value = Time.realtimeSinceStartup;

        }

        _clocktext.text = FormatTime(gameTime.Value);
            
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
