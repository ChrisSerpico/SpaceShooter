using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    // allows the character to be controlled via wasd/arrow keys

    // multiplicative speed modifier
    public float speed = 0.5f;

    // strength of gravity on the player
    public float gravity = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        // get the character controller of the player
        CharacterController cc = GetComponent<CharacterController>();

        // single vector for all movement
        Vector3 moveDir = Vector3.zero; // initialize to zero vector

        // only move laterally if the player is on the ground
        if (cc.isGrounded)
        {
            // Get lateral input
            float horizontal = Input.GetAxis("Horizontal");  // Left and Right
            float vertical = Input.GetAxis("Vertical");  // Forward and Back

            // combine the two movement axes into a single vector
            moveDir = new Vector3(horizontal, 0, vertical);
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= speed;
        }

        // gravity
        moveDir.y -= gravity * Time.deltaTime;

        // finally, move the player
        cc.Move(moveDir);

        // let the player scroll to select items
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        // get the player's inventory script
        Inventory inv = GetComponent<Inventory>();

        // select items
        if (scroll < 0)
            inv.SelectDown();
        else if (scroll > 0)
            inv.SelectUp();

        // use items
        float click = Input.GetAxis("Fire1");

        if (click == 1)
            inv.UseSelected();
	}
}
