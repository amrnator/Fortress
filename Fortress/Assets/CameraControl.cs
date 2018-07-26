using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    
    public float panSpeed = 10.0f;

    public float smoothSpeed = 0.125f;

    public Transform camera;

    public Vector3 target;

	// Use this for initialization
	void Awake () {
        camera = transform.GetChild(0);

        target = this.transform.position;
	}

    // TODO: Smooth camera movement
    // TODO: make input manager


    private void LateUpdate()
    {

    }

    public void pan(float hor, float vert)
    {
        Vector3 movement = new Vector3(hor, 0.0f, vert);
        transform.Translate(movement * panSpeed * Time.deltaTime);
    }

    public void zoomIn()
    {
        Vector3 zoom = new Vector3(0.0f, 0.0f, 2.0f);
        camera.transform.Translate(zoom, Space.Self);
    }

    public void zoomOut()
    {
        Vector3 zoom = new Vector3(0.0f, 0.0f, -2.0f);
        camera.transform.Translate(zoom, Space.Self);
    }

    public void rotateRight()
    {
        transform.Rotate(Vector3.up, 330f);
    }

    public void rotateLeft()
    {
        transform.Rotate(Vector3.up, 30f);
    }
}
