using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages all input from keyboard and calls specific methods
public class InputControl : MonoBehaviour {

    public CameraControl cameraControl;

    public float scrollSensitivity;

	// Use this for initialization
	void Start () {
		
	}
	
	void LateUpdate () {

        // Camera buttons

        // Pan
        cameraControl.pan(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        // Zoom
        if (Input.GetButtonDown("Zoom_in"))
        {
            cameraControl.zoomIn();
        }
        if (Input.GetButtonDown("Zoom_out"))
        {
            cameraControl.zoomOut();
        }

        // Rotations
        if(Input.GetButtonDown("RotateLeft"))
        {
            cameraControl.rotateLeft();
        }

        if (Input.GetButtonDown("RotateRight"))
        {
            cameraControl.rotateRight();
        }
	}
}
