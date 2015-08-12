using UnityEngine;
using System.Collections;

public class HeldItem : MonoBehaviour {

    // generic held item class
    // this represents items held in player's inventory 

    // image displayed in the inventory bar
    protected Sprite img;

    // cooldown, so the player can't just use an item 60 times a second
    public int maxCooldown;  // the cooldown length itself
    protected int cooldown;

    // the projectile that this item fires when used
    public Transform projectile;

    // initialize this item
    public void Initialize(Sprite image, int cdown)
    {
        img = image;

        maxCooldown = cdown;
        cooldown = 0; // start cooldown at 0 so the item can be used immediately
    }

    // update method
    // called every frame, mostly just deals with cooldown
    void Update()
    {
        if (cooldown > 0)
            cooldown--;
    }

    // get the sprite image
    public Sprite GetImg()
    {
        return img;
    }

    // use this item
    // will fire an object if the gameobject has a "gun" component
    public void Use()
    {
        if (cooldown <= 0)
        {
            Debug.Log("Item \"" + this.name + "\" was used.");
            
            // check to see whether this item has a projectile to fire
            if (this.projectile != null)
            {
                // if it does, fire it
                GameObject p = GameObject.Find("Player");  // get the player object
                Instantiate(projectile, p.transform.position, p.transform.rotation);
            }
            cooldown = maxCooldown;
        }
    }
}
