using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    bool moving = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            moving = true;
        else if (Input.GetMouseButtonUp(1))
            moving = false;

        if (moving)
            MoveCamera();
    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Debug.Log("Mouse X = " + mouseX + ", Mouse Y = " + mouseY);
    }
}
