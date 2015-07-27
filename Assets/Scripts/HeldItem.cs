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
    // can be extended by child classes
    public virtual void Use()
    {
        if (cooldown <= 0)
        {
            Debug.Log("Item \"" + this.name + "\" was used.");
            cooldown = maxCooldown;
        }
    }
}
