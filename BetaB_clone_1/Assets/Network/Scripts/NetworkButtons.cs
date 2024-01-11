using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Device;

public class NetworkButtons : MonoBehaviour
{
    void OnGUI()
    {
        float screenWidth = UnityEngine.Screen.width;
        float screenHeight = UnityEngine.Screen.height;
        float buttonWidth = 500f;
        float buttonHeight = 100f;

        float centerX = (screenWidth - buttonWidth) / 2;
        float centerY = (screenHeight - buttonHeight * 2) / 2;

        GUILayout.BeginArea(new Rect(centerX, centerY, buttonWidth, buttonHeight * 2));

        GUIStyle NetworkButton = new GUIStyle(GUI.skin.button);
        NetworkButton.fontSize = 60;

        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Control Room", NetworkButton, GUILayout.Height(buttonHeight)))
            {
                NetworkManager.Singleton.StartHost();
            }
            if (GUILayout.Button("Responder", NetworkButton, GUILayout.Height(buttonHeight)))
            {
                NetworkManager.Singleton.StartClient();
            }
        }

        GUILayout.EndArea();
    }
}
