using UnityEngine;
using System.Collections;

public class GroundItem : MonoBehaviour {

    // generic ground item class, makes them bounce and allows them to be picked up
    // this only represents items in the game world

	// the cooldown of this item when picked up and used by the player
    public int cooldown = 60;

    // the projectile that this item fires when used after being picked up
    public Transform projectile; 
    
    // Use this for initialization
	void Start () {
        // make the item bounce
        // uses GoKit! Google it for documentation
        Vector3[] points = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, .25f, 0), new Vector3(0, 0, 0) };  // this path uses relative values rather than world values
        GoSpline path = new GoSpline(points);

        Go.to(transform, 2f, new GoTweenConfig().positionPath(path, true).setIterations(-1));
	}

    // items are picked up when a player with inventory space comes into contact with it
    void OnTriggerEnter(Collider other)
    {
        // get the other collider's inventory
        Inventory i = other.gameObject.GetComponent<Inventory>();

        // make sure the other collider HAD an inventory, and then make sure it isn't full
        if (i != null && !i.IsFull())
        {
            i.AddItem(this);
        }
    }
}
