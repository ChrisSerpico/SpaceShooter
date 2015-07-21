using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    // allows the character to be controlled via wasd/arrow keys

    // multiplicative speed modifier
    public float speed = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");  // Left and Right
        float vertical = Input.GetAxis("Vertical");  // Forward and Back

        // get the rigidbody of the player
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.MovePosition(transform.position + (transform.forward * speed * vertical) + (transform.right * speed * horizontal));
        //rb.MovePosition(transform.position + (transform.right * speed * horizontal));
	}
}
