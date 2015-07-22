using UnityEngine;
using System.Collections;

public class SpriteController : MonoBehaviour 
{
    // the transform the sprite should be looking at
    // probably the player
    public Transform lookAt;

    // Update is called once per frame
	void Update () 
    {
        if (lookAt == null)
        {
            lookAt = GameObject.Find("Player").transform;
        }

        transform.LookAt(new Vector3(lookAt.position.x, transform.position.y, lookAt.position.z));
	}
}
