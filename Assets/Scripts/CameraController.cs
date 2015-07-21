using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // Turns the camera left and right when the player looks around 

    // how fast the player rotates
    public float rotationSpeed = 1.0f;

	// Update is called once per frame
	void Update () 
    {
        float rotation = Input.GetAxis("Mouse X"); // get the amount of x-input
        transform.Rotate(0, rotation * rotationSpeed, 0);
	}
}
