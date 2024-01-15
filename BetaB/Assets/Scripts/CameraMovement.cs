using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    [Header("Look Sensitivity")]
    public float sensX = 2f;
    public float sensY = 2f;

    [Header("Clamping")]
    public float minY = -90f;
    public float maxY = 90f;

    [Header("Spectator")]
    public float spectatorMoveSpeed = 5f;

    private float rotX = 0f;
    private float rotY = 0f;

    private bool isSpectator = true;

    void Start()
    {
        // Lock the cursor in the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        rotX += Input.GetAxis("Mouse X") * sensX;
        rotY += Input.GetAxis("Mouse Y") * sensY;

        // Clamp the vertical rotation
        rotY = Mathf.Clamp(rotY, minY, maxY);

        if (isSpectator)
        {
            // Apply rotations using Quaternion.Euler
            transform.rotation = Quaternion.Euler(-rotY, rotX, 0);

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            float y = 0;

            if (Input.GetKey(KeyCode.Space))
                y = 1;
            else if (Input.GetKey(KeyCode.LeftShift))
                y = -1;

            Vector3 dir = transform.TransformDirection(new Vector3(x, y, z));
            transform.position += dir * spectatorMoveSpeed * Time.deltaTime;
        }
    }
}